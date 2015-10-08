using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using oht.lib;
using oht.Properties;

namespace oht
{
    public partial class FormExamples : Form
    {
        readonly string _fileIni = "oht.ini";

        private Ohtapi _api;
        public FormExamples()
        {
            InitializeComponent();

            Load();

        }

        private void checkRemember_CheckedChanged(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            if (checkRemember.Checked)
            {
                File.WriteAllText(_fileIni, textPublicKey.Text + "|" + textSecretKey.Text + "|" + (checkSandbox.Checked ? "1" : "0") + "|" + textResources.Text + "|"
                    + textGetQuoteResources.Text + "|" + combosource_language.Text + "|" + combotarget_language.Text + "|" + textProjectID.Text + "|" + comboExpertise.Text);
            }
            else
            {
                File.WriteAllText(_fileIni, "");
            }
        }
        void Load()
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
                    checkSandbox.Checked = (s[2] == "1" ? true : false);
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
            OpenFileDialog openFile = new OpenFileDialog(); ;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
                {
                    FileInfo file = new FileInfo(openFile.FileName);

                    var r = api.Resources(file.FullName, file.Name, "file_mime", "");
                    textResources.Text = String.Join(",", r.results);
                    textGetQuoteResources.Text = textResources.Text;
                    MessageBox.Show(r.ToString());
                }
            }
        }

        private void butResourcesContent_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog(); ;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
                {
                    FileInfo file = new FileInfo(openFile.FileName);

                    var content = Encoding.Default.GetString(File.ReadAllBytes(openFile.FileName));
                    var r = api.Resources("", file.Name, "file_mime", content);
                    textResourcesContent.Text = r.status.Code == 0 ? r.results[0] : "";
                    MessageBox.Show(r.ToString());
                }
            }
        }

        private void butGetResource_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textResources.Text))
            {
                tools.SetMsg("enter", textResources);
                return;
            }
            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.GetResource(textResources.Text);
                textFileName.Text = r.results.file_name;

                if (textFileName.Text == "")
                    textFileName.Text = String.Format("oht_{0}.txt", textResources.Text);
                MessageBox.Show(r.ToString());
            }
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

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.DownloadResource(textResources.Text);
                if (r.status.Code == 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog
                    {
                        FilterIndex = 1,
                        RestoreDirectory = true,
                        FileName = textFileName.Text
                    };

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog1.FileName, r.file);
                    }
                }

                MessageBox.Show(r.ToString());



            }
        }

        private void butSupportedLanguages_Click(object sender, EventArgs e)
        {
            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.SupportedLanguages();
                if (r.status.Code == 0)
                {
                    comboLanguage.Items.Clear();
                    combosource_language.Items.Clear();
                    combotarget_language.Items.Clear();
                    foreach (var str in r.results)
                    {
                        comboLanguage.Items.Add(str.LanguageCode + "|" + str.LanguageName);
                        combosource_language.Items.Add(str.LanguageCode);
                        combotarget_language.Items.Add(str.LanguageCode);
                    }
                }


                MessageBox.Show(r.ToString());
            }
        }

        private void butDetectLanguage_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textForDetectLanguage.Text))
            {
                tools.SetMsg("enter", textForDetectLanguage);
                return;
            }
            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.DetectLanguageViaMachineTranslation(textForDetectLanguage.Text);
                MessageBox.Show(r.ToString());
            }
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

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.GetQuote(textGetQuoteResources.Text, textWordcount.Text, combosource_language.Text, combotarget_language.Text);
                MessageBox.Show(r.ToString());
            }
        }

        private void butGetWordCount_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textGetQuoteResources.Text))
            {
                tools.SetMsg("enter", textGetQuoteResources);
                return;
            }


            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.GetWordCount(textGetQuoteResources.Text);
                textWordcount.Text = r.status.Code == 0 ? r.results.total.wordcount.ToString() : "";


                MessageBox.Show(r.ToString());
            }
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

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.TranslateViaMachineTranslation(combosource_language.Text, combotarget_language.Text, textContent.Text);
                MessageBox.Show(r.ToString());
            }
        }

        private void butSupportedLanguagePairs_Click(object sender, EventArgs e)
        {
            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.SupportedLanguagePairs();
                MessageBox.Show(r.ToString());
            }
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

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.SupportedExpertises(combosource_language.Text, combotarget_language.Text);
                if (r.status.Code == 0)
                {
                    comboExpertise.Items.Clear();

                    foreach (var str in r.results)
                    {
                        comboExpertise.Items.Add(str.code + "-" + str.name);
                    }
                }
                MessageBox.Show(r.ToString());
            }
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

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.CreateTranslationProject(combosource_language.Text, combotarget_language.Text, textGetQuoteResources.Text
                    , "marketing-consumer-media"
                    , "", "", "", "translation");

                if (r.status.Code == 0)
                {
                    textProjectID.Text = r.results.project_id.ToString();
                }
                MessageBox.Show(r.ToString());
            }
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

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.CreateProofreadingProjectSource(combosource_language.Text, textGetQuoteResources.Text, "", "", "marketing-consumer-media", "", "namne12");
                if (r.status.Code == 0)
                {
                    textProjectID.Text = r.results.project_id.ToString();
                }
                MessageBox.Show(r.ToString());
            }
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

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.CreateProofreadingProjectSourceAndTarget(combosource_language.Text, combotarget_language.Text, textGetQuoteResources.Text, "");
                MessageBox.Show(r.ToString());
            }
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


            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.CreateTranscriptionProject(combosource_language.Text, textGetQuoteResources.Text);
                if (r.status.Code == 0)
                {
                    textProjectID.Text = r.results.project_id.ToString();
                }
                MessageBox.Show(r.ToString());
            }
        }

        private void butGetProjectDetails_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.GetProjectDetails(textProjectID.Text);
                MessageBox.Show(r.ToString());
            }
        }

        private void butCancelProject_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.CancelProject(textProjectID.Text);
                MessageBox.Show(r.ToString());
            }
        }

        private void butGetProjectsComments_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.GetProjectsComments(textProjectID.Text);
                MessageBox.Show(r.ToString());
            }
        }

        private void butRetrieveProjectRatings_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.RetrieveProjectRatings(textProjectID.Text);
                MessageBox.Show(r.ToString());
            }
        }

        private void butPostNewComment_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textProjectID.Text))
            {
                tools.SetMsg("enter", textProjectID);
                return;
            }

            using (var api = new Ohtapi(textPublicKey.Text, textSecretKey.Text, checkSandbox.Checked))
            {
                var r = api.PostNewProjectComment(textProjectID.Text, textComment.Text);
                MessageBox.Show(r.ToString());
            }
        }





    }
}
