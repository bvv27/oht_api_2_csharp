namespace oht
{
    partial class FormExamples
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.butAccount = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textPublicKey = new System.Windows.Forms.TextBox();
            this.textSecretKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkRemember = new System.Windows.Forms.CheckBox();
            this.checkSandbox = new System.Windows.Forms.CheckBox();
            this.butResources = new System.Windows.Forms.Button();
            this.butResourcesContent = new System.Windows.Forms.Button();
            this.textResources = new System.Windows.Forms.TextBox();
            this.textResourcesContent = new System.Windows.Forms.TextBox();
            this.butGetResource = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.butDownloadResource = new System.Windows.Forms.Button();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboExpertise = new System.Windows.Forms.ComboBox();
            this.butSupportedExpertises = new System.Windows.Forms.Button();
            this.butSupportedLanguagePairs = new System.Windows.Forms.Button();
            this.comboLanguage = new System.Windows.Forms.ComboBox();
            this.butSupportedLanguages = new System.Windows.Forms.Button();
            this.textForDetectLanguage = new System.Windows.Forms.TextBox();
            this.butDetectLanguage = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.butGetWordCount = new System.Windows.Forms.Button();
            this.textWordcount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textGetQuoteResources = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butGetQuote = new System.Windows.Forms.Button();
            this.combotarget_language = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.combosource_language = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.butTranslateViaMachineTranslation = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textContent = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.butPostNewComment = new System.Windows.Forms.Button();
            this.textComment = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.butRetrieveProjectRatings = new System.Windows.Forms.Button();
            this.butGetProjectsComments = new System.Windows.Forms.Button();
            this.butCancelProject = new System.Windows.Forms.Button();
            this.textProjectID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.butGetProjectDetails = new System.Windows.Forms.Button();
            this.butCreateTranscriptionProject = new System.Windows.Forms.Button();
            this.butCreateTranslationProjectSaT = new System.Windows.Forms.Button();
            this.butCreateProofreadingProject = new System.Windows.Forms.Button();
            this.butCreateTranslationProject = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSignIn = new System.Windows.Forms.Button();
            this.butPostProjectRatings = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butAccount
            // 
            this.butAccount.Location = new System.Drawing.Point(3, 9);
            this.butAccount.Name = "butAccount";
            this.butAccount.Size = new System.Drawing.Size(122, 28);
            this.butAccount.TabIndex = 0;
            this.butAccount.Text = "Account";
            this.butAccount.UseVisualStyleBackColor = true;
            this.butAccount.Click += new System.EventHandler(this.butAccount_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "public_key";
            // 
            // textPublicKey
            // 
            this.textPublicKey.Location = new System.Drawing.Point(76, 6);
            this.textPublicKey.Name = "textPublicKey";
            this.textPublicKey.Size = new System.Drawing.Size(250, 20);
            this.textPublicKey.TabIndex = 2;
            // 
            // textSecretKey
            // 
            this.textSecretKey.Location = new System.Drawing.Point(395, 6);
            this.textSecretKey.Name = "textSecretKey";
            this.textSecretKey.Size = new System.Drawing.Size(250, 20);
            this.textSecretKey.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(331, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "secret_key";
            // 
            // checkRemember
            // 
            this.checkRemember.AutoSize = true;
            this.checkRemember.Location = new System.Drawing.Point(753, 8);
            this.checkRemember.Name = "checkRemember";
            this.checkRemember.Size = new System.Drawing.Size(72, 17);
            this.checkRemember.TabIndex = 5;
            this.checkRemember.Text = "remember";
            this.checkRemember.UseVisualStyleBackColor = true;
            this.checkRemember.CheckedChanged += new System.EventHandler(this.checkRemember_CheckedChanged);
            // 
            // checkSandbox
            // 
            this.checkSandbox.AutoSize = true;
            this.checkSandbox.Location = new System.Drawing.Point(651, 8);
            this.checkSandbox.Name = "checkSandbox";
            this.checkSandbox.Size = new System.Drawing.Size(96, 17);
            this.checkSandbox.TabIndex = 6;
            this.checkSandbox.Text = "using Sandbox";
            this.checkSandbox.UseVisualStyleBackColor = true;
            // 
            // butResources
            // 
            this.butResources.Location = new System.Drawing.Point(6, 19);
            this.butResources.Name = "butResources";
            this.butResources.Size = new System.Drawing.Size(122, 28);
            this.butResources.TabIndex = 7;
            this.butResources.Text = "CreateFileResource";
            this.butResources.UseVisualStyleBackColor = true;
            this.butResources.Click += new System.EventHandler(this.butResources_Click);
            // 
            // butResourcesContent
            // 
            this.butResourcesContent.Location = new System.Drawing.Point(6, 53);
            this.butResourcesContent.Name = "butResourcesContent";
            this.butResourcesContent.Size = new System.Drawing.Size(166, 28);
            this.butResourcesContent.TabIndex = 8;
            this.butResourcesContent.Text = "CreateFileResource (content)";
            this.butResourcesContent.UseVisualStyleBackColor = true;
            this.butResourcesContent.Click += new System.EventHandler(this.butResourcesContent_Click);
            // 
            // textResources
            // 
            this.textResources.Location = new System.Drawing.Point(134, 23);
            this.textResources.Name = "textResources";
            this.textResources.Size = new System.Drawing.Size(250, 20);
            this.textResources.TabIndex = 9;
            // 
            // textResourcesContent
            // 
            this.textResourcesContent.Location = new System.Drawing.Point(181, 58);
            this.textResourcesContent.Name = "textResourcesContent";
            this.textResourcesContent.Size = new System.Drawing.Size(250, 20);
            this.textResourcesContent.TabIndex = 10;
            // 
            // butGetResource
            // 
            this.butGetResource.Location = new System.Drawing.Point(389, 19);
            this.butGetResource.Name = "butGetResource";
            this.butGetResource.Size = new System.Drawing.Size(122, 28);
            this.butGetResource.TabIndex = 11;
            this.butGetResource.Text = "Get resource";
            this.butGetResource.UseVisualStyleBackColor = true;
            this.butGetResource.Click += new System.EventHandler(this.butGetResource_Click);
            // 
            // butDownloadResource
            // 
            this.butDownloadResource.Location = new System.Drawing.Point(645, 19);
            this.butDownloadResource.Name = "butDownloadResource";
            this.butDownloadResource.Size = new System.Drawing.Size(122, 28);
            this.butDownloadResource.TabIndex = 12;
            this.butDownloadResource.Text = "Download resource";
            this.butDownloadResource.UseVisualStyleBackColor = true;
            this.butDownloadResource.Click += new System.EventHandler(this.butDownloadResource_Click);
            // 
            // textFileName
            // 
            this.textFileName.Location = new System.Drawing.Point(517, 24);
            this.textFileName.Name = "textFileName";
            this.textFileName.Size = new System.Drawing.Size(122, 20);
            this.textFileName.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butResources);
            this.groupBox1.Controls.Add(this.textFileName);
            this.groupBox1.Controls.Add(this.butResourcesContent);
            this.groupBox1.Controls.Add(this.butDownloadResource);
            this.groupBox1.Controls.Add(this.textResources);
            this.groupBox1.Controls.Add(this.butGetResource);
            this.groupBox1.Controls.Add(this.textResourcesContent);
            this.groupBox1.Location = new System.Drawing.Point(130, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(788, 90);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resources";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboExpertise);
            this.groupBox2.Controls.Add(this.butSupportedExpertises);
            this.groupBox2.Controls.Add(this.butSupportedLanguagePairs);
            this.groupBox2.Controls.Add(this.comboLanguage);
            this.groupBox2.Controls.Add(this.butSupportedLanguages);
            this.groupBox2.Location = new System.Drawing.Point(3, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(915, 51);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Discover";
            // 
            // comboExpertise
            // 
            this.comboExpertise.FormattingEnabled = true;
            this.comboExpertise.Location = new System.Drawing.Point(741, 20);
            this.comboExpertise.Name = "comboExpertise";
            this.comboExpertise.Size = new System.Drawing.Size(153, 21);
            this.comboExpertise.TabIndex = 24;
            // 
            // butSupportedExpertises
            // 
            this.butSupportedExpertises.Location = new System.Drawing.Point(557, 16);
            this.butSupportedExpertises.Name = "butSupportedExpertises";
            this.butSupportedExpertises.Size = new System.Drawing.Size(182, 28);
            this.butSupportedExpertises.TabIndex = 12;
            this.butSupportedExpertises.Text = "Supported expertises";
            this.butSupportedExpertises.UseVisualStyleBackColor = true;
            this.butSupportedExpertises.Click += new System.EventHandler(this.butSupportedExpertises_Click);
            // 
            // butSupportedLanguagePairs
            // 
            this.butSupportedLanguagePairs.Location = new System.Drawing.Point(369, 16);
            this.butSupportedLanguagePairs.Name = "butSupportedLanguagePairs";
            this.butSupportedLanguagePairs.Size = new System.Drawing.Size(182, 28);
            this.butSupportedLanguagePairs.TabIndex = 11;
            this.butSupportedLanguagePairs.Text = "Supported language pairs ";
            this.butSupportedLanguagePairs.UseVisualStyleBackColor = true;
            this.butSupportedLanguagePairs.Click += new System.EventHandler(this.butSupportedLanguagePairs_Click);
            // 
            // comboLanguage
            // 
            this.comboLanguage.FormattingEnabled = true;
            this.comboLanguage.Location = new System.Drawing.Point(142, 20);
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.Size = new System.Drawing.Size(215, 21);
            this.comboLanguage.TabIndex = 10;
            // 
            // butSupportedLanguages
            // 
            this.butSupportedLanguages.Location = new System.Drawing.Point(10, 16);
            this.butSupportedLanguages.Name = "butSupportedLanguages";
            this.butSupportedLanguages.Size = new System.Drawing.Size(129, 28);
            this.butSupportedLanguages.TabIndex = 9;
            this.butSupportedLanguages.Text = "Supported languages ";
            this.butSupportedLanguages.UseVisualStyleBackColor = true;
            this.butSupportedLanguages.Click += new System.EventHandler(this.butSupportedLanguages_Click);
            // 
            // textForDetectLanguage
            // 
            this.textForDetectLanguage.Location = new System.Drawing.Point(545, 21);
            this.textForDetectLanguage.Name = "textForDetectLanguage";
            this.textForDetectLanguage.Size = new System.Drawing.Size(129, 20);
            this.textForDetectLanguage.TabIndex = 14;
            this.textForDetectLanguage.Text = "black tree";
            // 
            // butDetectLanguage
            // 
            this.butDetectLanguage.Location = new System.Drawing.Point(679, 17);
            this.butDetectLanguage.Name = "butDetectLanguage";
            this.butDetectLanguage.Size = new System.Drawing.Size(215, 28);
            this.butDetectLanguage.TabIndex = 11;
            this.butDetectLanguage.Text = "Detect language via machine translation";
            this.butDetectLanguage.UseVisualStyleBackColor = true;
            this.butDetectLanguage.Click += new System.EventHandler(this.butDetectLanguage_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.butGetWordCount);
            this.groupBox3.Controls.Add(this.textWordcount);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textGetQuoteResources);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.butGetQuote);
            this.groupBox3.Location = new System.Drawing.Point(3, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(915, 51);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tools";
            // 
            // butGetWordCount
            // 
            this.butGetWordCount.Location = new System.Drawing.Point(392, 13);
            this.butGetWordCount.Name = "butGetWordCount";
            this.butGetWordCount.Size = new System.Drawing.Size(106, 28);
            this.butGetWordCount.TabIndex = 25;
            this.butGetWordCount.Text = "Get word count";
            this.butGetWordCount.UseVisualStyleBackColor = true;
            this.butGetWordCount.Click += new System.EventHandler(this.butGetWordCount_Click);
            // 
            // textWordcount
            // 
            this.textWordcount.Location = new System.Drawing.Point(318, 17);
            this.textWordcount.Name = "textWordcount";
            this.textWordcount.Size = new System.Drawing.Size(48, 20);
            this.textWordcount.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "wordcount ";
            // 
            // textGetQuoteResources
            // 
            this.textGetQuoteResources.Location = new System.Drawing.Point(61, 16);
            this.textGetQuoteResources.Name = "textGetQuoteResources";
            this.textGetQuoteResources.Size = new System.Drawing.Size(182, 20);
            this.textGetQuoteResources.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "resources";
            // 
            // butGetQuote
            // 
            this.butGetQuote.Location = new System.Drawing.Point(504, 12);
            this.butGetQuote.Name = "butGetQuote";
            this.butGetQuote.Size = new System.Drawing.Size(75, 28);
            this.butGetQuote.TabIndex = 10;
            this.butGetQuote.Text = "Get quote";
            this.butGetQuote.UseVisualStyleBackColor = true;
            this.butGetQuote.Click += new System.EventHandler(this.butGetQuote_Click);
            // 
            // combotarget_language
            // 
            this.combotarget_language.FormattingEnabled = true;
            this.combotarget_language.Location = new System.Drawing.Point(287, 162);
            this.combotarget_language.Name = "combotarget_language";
            this.combotarget_language.Size = new System.Drawing.Size(83, 21);
            this.combotarget_language.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(199, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "target_language ";
            // 
            // combosource_language
            // 
            this.combosource_language.FormattingEnabled = true;
            this.combosource_language.Location = new System.Drawing.Point(111, 162);
            this.combosource_language.Name = "combosource_language";
            this.combosource_language.Size = new System.Drawing.Size(83, 21);
            this.combosource_language.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "source_language ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.butTranslateViaMachineTranslation);
            this.groupBox4.Controls.Add(this.textForDetectLanguage);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textContent);
            this.groupBox4.Controls.Add(this.butDetectLanguage);
            this.groupBox4.Location = new System.Drawing.Point(3, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(915, 50);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Machine Translation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(516, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "text";
            // 
            // butTranslateViaMachineTranslation
            // 
            this.butTranslateViaMachineTranslation.Location = new System.Drawing.Point(284, 17);
            this.butTranslateViaMachineTranslation.Name = "butTranslateViaMachineTranslation";
            this.butTranslateViaMachineTranslation.Size = new System.Drawing.Size(181, 28);
            this.butTranslateViaMachineTranslation.TabIndex = 26;
            this.butTranslateViaMachineTranslation.Text = "Translate via machine translation";
            this.butTranslateViaMachineTranslation.UseVisualStyleBackColor = true;
            this.butTranslateViaMachineTranslation.Click += new System.EventHandler(this.butTranslateViaMachineTranslation_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "resource content";
            // 
            // textContent
            // 
            this.textContent.Location = new System.Drawing.Point(96, 21);
            this.textContent.Name = "textContent";
            this.textContent.Size = new System.Drawing.Size(182, 20);
            this.textContent.TabIndex = 15;
            this.textContent.Text = "green sail";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.butPostProjectRatings);
            this.groupBox5.Controls.Add(this.butPostNewComment);
            this.groupBox5.Controls.Add(this.textComment);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.butRetrieveProjectRatings);
            this.groupBox5.Controls.Add(this.butGetProjectsComments);
            this.groupBox5.Controls.Add(this.butCancelProject);
            this.groupBox5.Controls.Add(this.textProjectID);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.butGetProjectDetails);
            this.groupBox5.Controls.Add(this.butCreateTranscriptionProject);
            this.groupBox5.Controls.Add(this.butCreateTranslationProjectSaT);
            this.groupBox5.Controls.Add(this.butCreateProofreadingProject);
            this.groupBox5.Controls.Add(this.butCreateTranslationProject);
            this.groupBox5.Location = new System.Drawing.Point(6, 313);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(912, 119);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Project";
            // 
            // butPostNewComment
            // 
            this.butPostNewComment.Location = new System.Drawing.Point(188, 80);
            this.butPostNewComment.Name = "butPostNewComment";
            this.butPostNewComment.Size = new System.Drawing.Size(166, 28);
            this.butPostNewComment.TabIndex = 38;
            this.butPostNewComment.Text = "Post a new project comment ";
            this.butPostNewComment.UseVisualStyleBackColor = true;
            this.butPostNewComment.Click += new System.EventHandler(this.butPostNewComment_Click);
            // 
            // textComment
            // 
            this.textComment.Location = new System.Drawing.Point(87, 85);
            this.textComment.Name = "textComment";
            this.textComment.Size = new System.Drawing.Size(95, 20);
            this.textComment.TabIndex = 37;
            this.textComment.Text = "well";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "content (text)";
            // 
            // butRetrieveProjectRatings
            // 
            this.butRetrieveProjectRatings.Location = new System.Drawing.Point(427, 53);
            this.butRetrieveProjectRatings.Name = "butRetrieveProjectRatings";
            this.butRetrieveProjectRatings.Size = new System.Drawing.Size(128, 28);
            this.butRetrieveProjectRatings.TabIndex = 35;
            this.butRetrieveProjectRatings.Text = "Retrieve project ratings";
            this.butRetrieveProjectRatings.UseVisualStyleBackColor = true;
            this.butRetrieveProjectRatings.Click += new System.EventHandler(this.butRetrieveProjectRatings_Click);
            // 
            // butGetProjectsComments
            // 
            this.butGetProjectsComments.Location = new System.Drawing.Point(561, 53);
            this.butGetProjectsComments.Name = "butGetProjectsComments";
            this.butGetProjectsComments.Size = new System.Drawing.Size(128, 28);
            this.butGetProjectsComments.TabIndex = 34;
            this.butGetProjectsComments.Text = "Get project’s comments";
            this.butGetProjectsComments.UseVisualStyleBackColor = true;
            this.butGetProjectsComments.Click += new System.EventHandler(this.butGetProjectsComments_Click);
            // 
            // butCancelProject
            // 
            this.butCancelProject.Location = new System.Drawing.Point(315, 53);
            this.butCancelProject.Name = "butCancelProject";
            this.butCancelProject.Size = new System.Drawing.Size(106, 28);
            this.butCancelProject.TabIndex = 33;
            this.butCancelProject.Text = "Cancel project";
            this.butCancelProject.UseVisualStyleBackColor = true;
            this.butCancelProject.Click += new System.EventHandler(this.butCancelProject_Click);
            // 
            // textProjectID
            // 
            this.textProjectID.Location = new System.Drawing.Point(71, 58);
            this.textProjectID.Name = "textProjectID";
            this.textProjectID.Size = new System.Drawing.Size(95, 20);
            this.textProjectID.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "project_id";
            // 
            // butGetProjectDetails
            // 
            this.butGetProjectDetails.Location = new System.Drawing.Point(173, 53);
            this.butGetProjectDetails.Name = "butGetProjectDetails";
            this.butGetProjectDetails.Size = new System.Drawing.Size(137, 28);
            this.butGetProjectDetails.TabIndex = 30;
            this.butGetProjectDetails.Text = "Get project details";
            this.butGetProjectDetails.UseVisualStyleBackColor = true;
            this.butGetProjectDetails.Click += new System.EventHandler(this.butGetProjectDetails_Click);
            // 
            // butCreateTranscriptionProject
            // 
            this.butCreateTranscriptionProject.Location = new System.Drawing.Point(622, 19);
            this.butCreateTranscriptionProject.Name = "butCreateTranscriptionProject";
            this.butCreateTranscriptionProject.Size = new System.Drawing.Size(168, 28);
            this.butCreateTranscriptionProject.TabIndex = 29;
            this.butCreateTranscriptionProject.Text = "Create a transcription project";
            this.butCreateTranscriptionProject.UseVisualStyleBackColor = true;
            this.butCreateTranscriptionProject.Click += new System.EventHandler(this.butCreateTranscriptionProject_Click);
            // 
            // butCreateTranslationProjectSaT
            // 
            this.butCreateTranslationProjectSaT.Location = new System.Drawing.Point(378, 19);
            this.butCreateTranslationProjectSaT.Name = "butCreateTranslationProjectSaT";
            this.butCreateTranslationProjectSaT.Size = new System.Drawing.Size(241, 28);
            this.butCreateTranslationProjectSaT.TabIndex = 28;
            this.butCreateTranslationProjectSaT.Text = "Create proofreading project  (source and target)";
            this.butCreateTranslationProjectSaT.UseVisualStyleBackColor = true;
            this.butCreateTranslationProjectSaT.Click += new System.EventHandler(this.butCreateTranslationProjectSaT_Click);
            // 
            // butCreateProofreadingProject
            // 
            this.butCreateProofreadingProject.Location = new System.Drawing.Point(173, 19);
            this.butCreateProofreadingProject.Name = "butCreateProofreadingProject";
            this.butCreateProofreadingProject.Size = new System.Drawing.Size(202, 28);
            this.butCreateProofreadingProject.TabIndex = 27;
            this.butCreateProofreadingProject.Text = "Create proofreading project  (source)";
            this.butCreateProofreadingProject.UseVisualStyleBackColor = true;
            this.butCreateProofreadingProject.Click += new System.EventHandler(this.butCreateProofreadingProject_Click);
            // 
            // butCreateTranslationProject
            // 
            this.butCreateTranslationProject.Location = new System.Drawing.Point(10, 19);
            this.butCreateTranslationProject.Name = "butCreateTranslationProject";
            this.butCreateTranslationProject.Size = new System.Drawing.Size(156, 28);
            this.butCreateTranslationProject.TabIndex = 26;
            this.butCreateTranslationProject.Text = "Create translation project";
            this.butCreateTranslationProject.UseVisualStyleBackColor = true;
            this.butCreateTranslationProject.Click += new System.EventHandler(this.butCreateTranslationProject_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butAccount);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.combotarget_language);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.combosource_language);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(-3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 491);
            this.panel1.TabIndex = 25;
            // 
            // butSignIn
            // 
            this.butSignIn.Location = new System.Drawing.Point(831, 2);
            this.butSignIn.Name = "butSignIn";
            this.butSignIn.Size = new System.Drawing.Size(84, 28);
            this.butSignIn.TabIndex = 26;
            this.butSignIn.Text = "Sign in";
            this.butSignIn.UseVisualStyleBackColor = true;
            this.butSignIn.Click += new System.EventHandler(this.butSignIn_Click);
            // 
            // butPostProjectRatings
            // 
            this.butPostProjectRatings.Location = new System.Drawing.Point(360, 80);
            this.butPostProjectRatings.Name = "butPostProjectRatings";
            this.butPostProjectRatings.Size = new System.Drawing.Size(135, 28);
            this.butPostProjectRatings.TabIndex = 39;
            this.butPostProjectRatings.Text = "Post project ratings ";
            this.butPostProjectRatings.UseVisualStyleBackColor = true;
            this.butPostProjectRatings.Click += new System.EventHandler(this.butPostProjectRatings_Click);
            // 
            // FormExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 523);
            this.Controls.Add(this.butSignIn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkSandbox);
            this.Controls.Add(this.checkRemember);
            this.Controls.Add(this.textSecretKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textPublicKey);
            this.Controls.Add(this.label1);
            this.Name = "FormExamples";
            this.Text = "One Hour Translation API Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExamples_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPublicKey;
        private System.Windows.Forms.TextBox textSecretKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkRemember;
        private System.Windows.Forms.CheckBox checkSandbox;
        private System.Windows.Forms.Button butResources;
        private System.Windows.Forms.Button butResourcesContent;
        private System.Windows.Forms.TextBox textResources;
        private System.Windows.Forms.TextBox textResourcesContent;
        private System.Windows.Forms.Button butGetResource;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button butDownloadResource;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button butSupportedLanguages;
        private System.Windows.Forms.ComboBox comboLanguage;
        private System.Windows.Forms.Button butDetectLanguage;
        private System.Windows.Forms.TextBox textForDetectLanguage;
        private System.Windows.Forms.ComboBox combotarget_language;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combosource_language;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textWordcount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butGetQuote;
        private System.Windows.Forms.Button butGetWordCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textContent;
        private System.Windows.Forms.Button butTranslateViaMachineTranslation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button butSupportedLanguagePairs;
        private System.Windows.Forms.Button butSupportedExpertises;
        private System.Windows.Forms.Button butCreateTranslationProject;
        private System.Windows.Forms.TextBox textGetQuoteResources;
        private System.Windows.Forms.Button butCreateProofreadingProject;
        private System.Windows.Forms.Button butCreateTranslationProjectSaT;
        private System.Windows.Forms.Button butCreateTranscriptionProject;
        private System.Windows.Forms.Button butGetProjectDetails;
        private System.Windows.Forms.TextBox textProjectID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button butCancelProject;
        private System.Windows.Forms.Button butGetProjectsComments;
        private System.Windows.Forms.Button butRetrieveProjectRatings;
        private System.Windows.Forms.TextBox textComment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button butPostNewComment;
        private System.Windows.Forms.ComboBox comboExpertise;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butSignIn;
        private System.Windows.Forms.Button butPostProjectRatings;
    }
}

