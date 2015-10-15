using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using oht.lib;

namespace oht
{
    public partial class FormExamples : Form
    {
        readonly string _fileIni = "oht.ini";

        private Ohtapi _api;
        public FormExamples()
        {
            InitializeComponent();

            LoadIni();

        }

        private void checkRemember_CheckedChanged(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            if (checkRemember.Checked)
            {
                File.WriteAllText(_fileIni, textPublicKey.Text + @"|" + textSecretKey.Text + @"|" + (checkSandbox.Checked ? "1" : "0")
                    + @"|" + textResources.Text + @"|" + textGetQuoteResources.Text + @"|" + combosource_language.Text
                    + @"|" + combotarget_language.Text + @"|" + textProjectID.Text + @"|" + comboExpertise.Text);
            }
            else
            {
                File.WriteAllText(_fileIni, "");
            }
        }

        void LoadIni()
        {
            if (File.Exists(_fileIni))
            {
                string[] s = (File.ReadAllText(_fileIni) + "|||||||||").Split('|');
                textPublicKey.Text = s[0];
                textSecretKey.Text = s[1];

                textResources.Text = s[3];
                textGetQuoteResources.Text = s[4];
                combosource_language.Text = s[5];
                combotarget_language.Text = s[6];
                textProjectID.Text = s[7];
                comboExpertise.Text = s[8];

                if (s[2] != "")
                {
                    checkSandbox.Checked = (s[2] == "1");
                    checkRemember.Checked = true;
                }
            }
        }

        private void FormExamples_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void butSignIn_Click(object sender, EventArgs e)
        {
            _api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked);
            panel1.Enabled = true;
        }

        private void butAccount_Click(object sender, EventArgs e)
        {
            var r = _api.Account();
            MessageBox.Show(r.ToString());
        }

        private void butResources_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFile.FileName);

                var r = _api.CreateFileResources(file.FullName, file.Name, "file_mime","tree");
                textResources.Text = String.Join(",", r.Results);
                textGetQuoteResources.Text = textResources.Text;
                MessageBox.Show(r.ToString());
            }
        }

        private void butResourcesContent_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFile.FileName);

                var content = Encoding.Default.GetString(File.ReadAllBytes(openFile.FileName));
                var r = _api.CreateFileResources("", file.Name, "file_mime", content);
                textResourcesContent.Text = r.Status.Code == 0 ? r.Results[0] : "";
                MessageBox.Show(r.ToString());
            }
        }

        private void butGetResource_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textResources.Text))
            {
                tools.SetMsg("enter", textResources);
                return;
            }

            var r = _api.GetResource(textResources.Text);
            textFileName.Text = r.Result.FileName;

            if (textFileName.Text == "")
                textFileName.Text = String.Format("oht_{0}.txt", textResources.Text);
            MessageBox.Show(r.ToString());

        }

        private void butDownloadResource_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textResources.Text))
            {
                tools.SetMsg("enter", textResources);
                return;
            }

            if (String.IsNullOrWhiteSpace(textFileName.Text))
            {
                tools.SetMsg("enter", textFileName);
                return;
            }


            var r = _api.DownloadResource(textResources.Text);
            if (r.Status.Code == 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    FilterIndex = 1,
                    RestoreDirectory = true,
                    FileName = textFileName.Text
                };

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog1.FileName, r.File);
                }
            }

            MessageBox.Show(r.ToString());
        }

        private void butSupportedLanguages_Click(object sender, EventArgs e)
        {
            var r = _api.SupportedLanguages();
            if (r.Status.Code == 0)
            {
                comboLanguage.Items.Clear();
                combosource_language.Items.Clear();
                combotarget_language.Items.Clear();
                foreach (var str in r.Results)
                {
                    comboLanguage.Items.Add(str.LanguageCode + "|" + str.LanguageName);
                    combosource_language.Items.Add(str.LanguageCode);
                    combotarget_language.Items.Add(str.LanguageCode);
                }
            }
            MessageBox.Show(r.ToString());
        }

        private void butDetectLanguage_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textForDetectLanguage.Text))
            {
                tools.SetMsg("enter", textForDetectLanguage);
                return;
            }

            var r = _api.DetectLanguageViaMachineTranslation(textForDetectLanguage.Text);
            MessageBox.Show(r.ToString());

        }

        private void butGetQuote_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textGetQuoteResources.Text))
            {
                tools.SetMsg("enter", textGetQuoteResources);
                return;
            }

            if (!String.IsNullOrWhiteSpace(textWordcount.Text))
            {
                long num;
                if (!Int64.TryParse(textWordcount.Text, out num))
                {
                    textWordcount.Text = "";
                    tools.SetMsg("enter integer or empty", textGetQuoteResources);
                    return;
                }
            }


            if (String.IsNullOrWhiteSpace(combosource_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combosource_language);
                return;
            }
            if (String.IsNullOrWhiteSpace(combotarget_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combotarget_language);
                return;
            }


            var r = _api.GetQuote(textGetQuoteResources.Text, textWordcount.Text, combosource_language.Text, combotarget_language.Text);
            MessageBox.Show(r.ToString());

        }

        private void butGetWordCount_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textGetQuoteResources.Text))
            {
                tools.SetMsg("enter", textGetQuoteResources);
                return;
            }



            var r = _api.GetWordCount(textGetQuoteResources.Text);
            textWordcount.Text = r.Status.Code == 0 ? r.Result.Total.Wordcount.ToString() : "";
            MessageBox.Show(r.ToString());

        }

        private void butTranslateViaMachineTranslation_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textContent.Text))
            {
                tools.SetMsg("enter", textContent);
                return;
            }

            if (String.IsNullOrWhiteSpace(combosource_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combosource_language);
                return;
            }
            if (String.IsNullOrWhiteSpace(combotarget_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combotarget_language);
                return;
            }


            var r = _api.TranslateViaMachineTranslation(combosource_language.Text, combotarget_language.Text, textContent.Text);
            MessageBox.Show(r.ToString());

        }

        private void butSupportedLanguagePairs_Click(object sender, EventArgs e)
        {
            var r = _api.SupportedLanguagePairs();
            MessageBox.Show(r.ToString());
        }

        private void butSupportedExpertises_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(combosource_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combosource_language);
                return;
            }
            if (String.IsNullOrWhiteSpace(combotarget_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combotarget_language);
                return;
            }


            var r = _api.SupportedExpertises(combosource_language.Text, combotarget_language.Text);
            if (r.Status.Code == 0)
            {
                comboExpertise.Items.Clear();

                foreach (var str in r.Results)
                {
                    comboExpertise.Items.Add(str.Code + "-" + str.Name);
                }
            }
            MessageBox.Show(r.ToString());

        }

        private void butCreateTranslationProject_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textGetQuoteResources.Text))
            {
                tools.SetMsg("click", butResources);
                tools.SetMsg("enter", textGetQuoteResources);
                return;
            }

            if (String.IsNullOrWhiteSpace(combosource_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combosource_language);
                return;
            }
            if (String.IsNullOrWhiteSpace(combotarget_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combotarget_language);
                return;
            }
            if (String.IsNullOrWhiteSpace(comboExpertise.Text))
            {
                tools.SetMsg("click", butSupportedExpertises);
                tools.SetMsg("enter", comboExpertise);
                return;
            }


            var r = _api.CreateTranslationProject(combosource_language.Text, combotarget_language.Text, textGetQuoteResources.Text
                , "marketing-consumer-media"
                , "", "", "", "translation");

            if (r.Status.Code == 0)
            {
                textProjectID.Text = r.Result.ProjectId.ToString();
            }
            MessageBox.Show(r.ToString());

        }

        private void butCreateProofreadingProject_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textGetQuoteResources.Text))
            {
                tools.SetMsg("click", butResources);
                tools.SetMsg("enter", textGetQuoteResources);
                return;
            }

            if (String.IsNullOrWhiteSpace(combosource_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combosource_language);
                return;
            }

            //http://sandbox.onehourtranslation.com/api/2/projects/proof-general?notes=&sources=rsc-5618b9e3e272e2-73253523&secret_key=35aec76f5d9a015304173d1d81891f65&expertise=marketing-consumer-media&name=unittest+proof_translated+2015-10-13+17%3A23-02&source_language=en-us&callbackUrl=&public_key=c7t9NbMpG2xK6nvD834B&wordCount=0
            var r = _api.CreateProofreadingProjectSource(combosource_language.Text, textGetQuoteResources.Text, "", "", "marketing-consumer-media", "", "namne12");
            if (r.Status.Code == 0)
            {
                textProjectID.Text = r.Result.ProjectId.ToString();
            }
            MessageBox.Show(r.ToString());

        }

        private void butCreateTranslationProjectSaT_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textGetQuoteResources.Text))
            {
                tools.SetMsg("click", butResources);
                tools.SetMsg("enter", textGetQuoteResources);
                return;
            }

            if (String.IsNullOrWhiteSpace(combosource_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combosource_language);
                return;
            }
            if (String.IsNullOrWhiteSpace(combotarget_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combotarget_language);
                return;
            }


            var r = _api.CreateProofreadingProjectSourceAndTarget(combosource_language.Text, combotarget_language.Text, "rsc-560e7ea4650793-27822858", "rsc-560ea011277cb3-18000700");
            MessageBox.Show(r.ToString());

        }

        private void butCreateTranscriptionProject_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textGetQuoteResources.Text))
            {
                tools.SetMsg("click", butResources);
                tools.SetMsg("enter", textGetQuoteResources);
                return;
            }

            if (String.IsNullOrWhiteSpace(combosource_language.Text))
            {
                tools.SetMsg("click", butSupportedLanguages);
                tools.SetMsg("enter", combosource_language);
                return;
            }



            var r = _api.CreateTranscriptionProject(combosource_language.Text, textGetQuoteResources.Text);
            if (r.Status.Code == 0)
            {
                textProjectID.Text = r.Result.ProjectId.ToString();
            }
            MessageBox.Show(r.ToString());

        }

        private void butGetProjectDetails_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }


            var r = _api.GetProjectDetails(textProjectID.Text);
            MessageBox.Show(r.ToString());

        }

        private void butCancelProject_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }


            var r = _api.CancelProject(textProjectID.Text);
            MessageBox.Show(r.ToString());

        }

        private void butGetProjectsComments_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }


            var r = _api.GetProjectsComments(textProjectID.Text);
            MessageBox.Show(r.ToString());

        }

        private void butRetrieveProjectRatings_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }


            var r = _api.RetrieveProjectRatings(textProjectID.Text);
            MessageBox.Show(r.ToString());

        }

        private void butPostNewComment_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }


            var r = _api.PostNewProjectComment(textProjectID.Text, textComment.Text);
            MessageBox.Show(r.ToString());

        }

        private void butPostProjectRatings_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }


            var r = _api.PostProjectRatings(textProjectID.Text,"Customer", 1, "remarks");
            MessageBox.Show(r.ToString());
        }





    }
}
