namespace UniversalFileExtractor
{
    partial class FileExtractor
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox textBoxStartString;
        private System.Windows.Forms.Label labelStartString;
        private System.Windows.Forms.TextBox textBoxEndString;
        private System.Windows.Forms.Label labelEndString;
        private System.Windows.Forms.TextBox textBoxOffsetString;
        private System.Windows.Forms.TextBox textBoxOffsetSequence;
        private System.Windows.Forms.TextBox textBoxOffsetLength;
        private System.Windows.Forms.Label labelOffsetLength;
        private System.Windows.Forms.RadioButton radioButtonOffsetString;
        private System.Windows.Forms.RadioButton radioButtonOffsetSequence;
        private System.Windows.Forms.TextBox textBoxTrimBytes;
        private System.Windows.Forms.Label labelTrimBytes;
        private System.Windows.Forms.TextBox textBoxTrimRepeatedByte;
        private System.Windows.Forms.Label labelTrimRepeatedByte;
        private System.Windows.Forms.ComboBox comboBoxTrimMode;
        private System.Windows.Forms.Label labelTrimMode;
        private System.Windows.Forms.TreeView treeViewModes;
        private System.Windows.Forms.ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            textBoxDirectoryPath = new TextBox();
            labelDirectoryPath = new Label();
            richTextBoxOutput = new RichTextBox();
            buttonExtract = new Button();
            buttonClearOutput = new Button();
            inputPanel = new Panel();
            comboBoxTrimMode = new ComboBox();
            labelTrimMode = new Label();
            textBoxHexAddress = new TextBox();
            labelHexAddress = new Label();
            textBoxStartAddress = new TextBox();
            labelStartAddress = new Label();
            textBoxEndAddress = new TextBox();
            labelEndAddress = new Label();
            textBoxStartSequence = new TextBox();
            labelStartSequence = new Label();
            textBoxEndSequence = new TextBox();
            labelEndSequence = new Label();
            textBoxStartString = new TextBox();
            labelStartString = new Label();
            textBoxEndString = new TextBox();
            labelEndString = new Label();
            textBoxOutputFormat = new TextBox();
            labelOutputFormat = new Label();
            radioButtonOffsetString = new RadioButton();
            radioButtonOffsetSequence = new RadioButton();
            textBoxOffsetString = new TextBox();
            textBoxOffsetSequence = new TextBox();
            textBoxOffsetLength = new TextBox();
            labelOffsetLength = new Label();
            textBoxTrimBytes = new TextBox();
            labelTrimBytes = new Label();
            textBoxTrimRepeatedByte = new TextBox();
            labelTrimRepeatedByte = new Label();
            treeViewModes = new TreeView();
            toolTip = new ToolTip(components);
            inputPanel.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxDirectoryPath
            // 
            textBoxDirectoryPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDirectoryPath.ForeColor = SystemColors.GrayText;
            textBoxDirectoryPath.Location = new Point(88, 12);
            textBoxDirectoryPath.Name = "textBoxDirectoryPath";
            textBoxDirectoryPath.Size = new Size(681, 23);
            textBoxDirectoryPath.TabIndex = 0;
            toolTip.SetToolTip(textBoxDirectoryPath, "拖放文件夹到此");
            textBoxDirectoryPath.TextChanged += textBoxDirectoryPath_TextChanged;
            textBoxDirectoryPath.Enter += TextBoxDirectoryPath_Enter;
            textBoxDirectoryPath.Leave += TextBoxDirectoryPath_Leave;
            // 
            // labelDirectoryPath
            // 
            labelDirectoryPath.AutoSize = true;
            labelDirectoryPath.ForeColor = Color.DodgerBlue;
            labelDirectoryPath.Location = new Point(12, 15);
            labelDirectoryPath.Name = "labelDirectoryPath";
            labelDirectoryPath.Size = new Size(68, 17);
            labelDirectoryPath.TabIndex = 1;
            labelDirectoryPath.Text = "文件夹路径";
            // 
            // richTextBoxOutput
            // 
            richTextBoxOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxOutput.Location = new Point(12, 424);
            richTextBoxOutput.Name = "richTextBoxOutput";
            richTextBoxOutput.Size = new Size(851, 321);
            richTextBoxOutput.TabIndex = 4;
            richTextBoxOutput.Text = "";
            toolTip.SetToolTip(richTextBoxOutput, "从源文件中提取出来的文件信息");
            // 
            // buttonExtract
            // 
            buttonExtract.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonExtract.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            buttonExtract.ForeColor = Color.Lime;
            buttonExtract.Location = new Point(775, 41);
            buttonExtract.Name = "buttonExtract";
            buttonExtract.Size = new Size(88, 73);
            buttonExtract.TabIndex = 3;
            buttonExtract.Text = "开始提取";
            toolTip.SetToolTip(buttonExtract, "点击此按钮开始解包提取");
            buttonExtract.UseVisualStyleBackColor = true;
            buttonExtract.Click += buttonExtract_Click;
            // 
            // buttonClearOutput
            // 
            buttonClearOutput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClearOutput.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            buttonClearOutput.ForeColor = Color.Orange;
            buttonClearOutput.Location = new Point(775, 138);
            buttonClearOutput.Name = "buttonClearOutput";
            buttonClearOutput.Size = new Size(88, 30);
            buttonClearOutput.TabIndex = 5;
            buttonClearOutput.Text = "清空日志";
            toolTip.SetToolTip(buttonClearOutput, "清空所有提取出来的文件信息");
            buttonClearOutput.UseVisualStyleBackColor = true;
            buttonClearOutput.Click += buttonClearOutput_Click;
            // 
            // inputPanel
            // 
            inputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputPanel.BorderStyle = BorderStyle.FixedSingle;
            inputPanel.Controls.Add(comboBoxTrimMode);
            inputPanel.Controls.Add(labelTrimMode);
            inputPanel.Controls.Add(textBoxHexAddress);
            inputPanel.Controls.Add(labelHexAddress);
            inputPanel.Controls.Add(textBoxStartAddress);
            inputPanel.Controls.Add(labelStartAddress);
            inputPanel.Controls.Add(textBoxEndAddress);
            inputPanel.Controls.Add(labelEndAddress);
            inputPanel.Controls.Add(textBoxStartSequence);
            inputPanel.Controls.Add(labelStartSequence);
            inputPanel.Controls.Add(textBoxEndSequence);
            inputPanel.Controls.Add(labelEndSequence);
            inputPanel.Controls.Add(textBoxStartString);
            inputPanel.Controls.Add(labelStartString);
            inputPanel.Controls.Add(textBoxEndString);
            inputPanel.Controls.Add(labelEndString);
            inputPanel.Controls.Add(textBoxOutputFormat);
            inputPanel.Controls.Add(labelOutputFormat);
            inputPanel.Controls.Add(radioButtonOffsetString);
            inputPanel.Controls.Add(radioButtonOffsetSequence);
            inputPanel.Controls.Add(textBoxOffsetString);
            inputPanel.Controls.Add(textBoxOffsetSequence);
            inputPanel.Controls.Add(textBoxOffsetLength);
            inputPanel.Controls.Add(labelOffsetLength);
            inputPanel.Controls.Add(textBoxTrimBytes);
            inputPanel.Controls.Add(labelTrimBytes);
            inputPanel.Controls.Add(textBoxTrimRepeatedByte);
            inputPanel.Controls.Add(labelTrimRepeatedByte);
            inputPanel.Location = new Point(283, 41);
            inputPanel.Name = "inputPanel";
            inputPanel.Size = new Size(486, 377);
            inputPanel.TabIndex = 7;
            toolTip.SetToolTip(inputPanel, "根据不同的模式启用不同预设参数");
            // 
            // comboBoxTrimMode
            // 
            comboBoxTrimMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTrimMode.FormattingEnabled = true;
            comboBoxTrimMode.Items.AddRange(new object[] { "无", "仅排除字节数", "仅排除重复字节", "两者都排除(字节数量优先)", "两者都排除(重复字节优先)" });
            comboBoxTrimMode.Location = new Point(130, 345);
            comboBoxTrimMode.Name = "comboBoxTrimMode";
            comboBoxTrimMode.Size = new Size(202, 25);
            comboBoxTrimMode.TabIndex = 44;
            comboBoxTrimMode.SelectedIndexChanged += ComboBoxTrimMode_SelectedIndexChanged;
            // 
            // labelTrimMode
            // 
            labelTrimMode.AutoSize = true;
            labelTrimMode.ForeColor = Color.Red;
            labelTrimMode.Location = new Point(3, 350);
            labelTrimMode.Name = "labelTrimMode";
            labelTrimMode.Size = new Size(56, 17);
            labelTrimMode.TabIndex = 45;
            labelTrimMode.Text = "排除模式";
            toolTip.SetToolTip(labelTrimMode, "选择一种模式来排除那些垃圾字节");
            // 
            // textBoxHexAddress
            // 
            textBoxHexAddress.Location = new Point(130, 125);
            textBoxHexAddress.Name = "textBoxHexAddress";
            textBoxHexAddress.Size = new Size(240, 23);
            textBoxHexAddress.TabIndex = 0;
            // 
            // labelHexAddress
            // 
            labelHexAddress.AutoSize = true;
            labelHexAddress.ForeColor = Color.Cyan;
            labelHexAddress.Location = new Point(3, 130);
            labelHexAddress.Name = "labelHexAddress";
            labelHexAddress.Size = new Size(70, 17);
            labelHexAddress.TabIndex = 21;
            labelHexAddress.Text = "16进制地址";
            toolTip.SetToolTip(labelHexAddress, "0x00000000或者00000000的输入方式都可以");
            labelHexAddress.Click += labelHexAddress_Click;
            // 
            // textBoxStartAddress
            // 
            textBoxStartAddress.Location = new Point(130, 95);
            textBoxStartAddress.Name = "textBoxStartAddress";
            textBoxStartAddress.Size = new Size(240, 23);
            textBoxStartAddress.TabIndex = 0;
            // 
            // labelStartAddress
            // 
            labelStartAddress.AutoSize = true;
            labelStartAddress.ForeColor = Color.Cyan;
            labelStartAddress.Location = new Point(3, 100);
            labelStartAddress.Name = "labelStartAddress";
            labelStartAddress.Size = new Size(56, 17);
            labelStartAddress.TabIndex = 7;
            labelStartAddress.Text = "起始地址";
            toolTip.SetToolTip(labelStartAddress, "0x00000000或者00000000的输入方式都可以");
            // 
            // textBoxEndAddress
            // 
            textBoxEndAddress.Location = new Point(130, 155);
            textBoxEndAddress.Name = "textBoxEndAddress";
            textBoxEndAddress.Size = new Size(240, 23);
            textBoxEndAddress.TabIndex = 1;
            // 
            // labelEndAddress
            // 
            labelEndAddress.AutoSize = true;
            labelEndAddress.ForeColor = Color.Cyan;
            labelEndAddress.Location = new Point(3, 160);
            labelEndAddress.Name = "labelEndAddress";
            labelEndAddress.Size = new Size(56, 17);
            labelEndAddress.TabIndex = 9;
            labelEndAddress.Text = "结束地址";
            toolTip.SetToolTip(labelEndAddress, "0x00000000或者00000000的输入方式都可以");
            // 
            // textBoxStartSequence
            // 
            textBoxStartSequence.Location = new Point(130, 35);
            textBoxStartSequence.Name = "textBoxStartSequence";
            textBoxStartSequence.Size = new Size(240, 23);
            textBoxStartSequence.TabIndex = 0;
            // 
            // labelStartSequence
            // 
            labelStartSequence.AutoSize = true;
            labelStartSequence.ForeColor = Color.Fuchsia;
            labelStartSequence.Location = new Point(3, 40);
            labelStartSequence.Name = "labelStartSequence";
            labelStartSequence.Size = new Size(80, 17);
            labelStartSequence.TabIndex = 11;
            labelStartSequence.Text = "起始字节序列";
            toolTip.SetToolTip(labelStartSequence, "89 50 4E 47或者89504E47两种输入方式都可以");
            // 
            // textBoxEndSequence
            // 
            textBoxEndSequence.Location = new Point(130, 65);
            textBoxEndSequence.Name = "textBoxEndSequence";
            textBoxEndSequence.Size = new Size(240, 23);
            textBoxEndSequence.TabIndex = 1;
            // 
            // labelEndSequence
            // 
            labelEndSequence.AutoSize = true;
            labelEndSequence.ForeColor = Color.Fuchsia;
            labelEndSequence.Location = new Point(3, 70);
            labelEndSequence.Name = "labelEndSequence";
            labelEndSequence.Size = new Size(80, 17);
            labelEndSequence.TabIndex = 13;
            labelEndSequence.Text = "结束字节序列";
            toolTip.SetToolTip(labelEndSequence, "89 50 4E 47或者89504E47两种输入方式都可以");
            // 
            // textBoxStartString
            // 
            textBoxStartString.Enabled = false;
            textBoxStartString.Location = new Point(130, 185);
            textBoxStartString.Name = "textBoxStartString";
            textBoxStartString.Size = new Size(240, 23);
            textBoxStartString.TabIndex = 0;
            // 
            // labelStartString
            // 
            labelStartString.AutoSize = true;
            labelStartString.ForeColor = Color.FromArgb(255, 128, 0);
            labelStartString.Location = new Point(3, 190);
            labelStartString.Name = "labelStartString";
            labelStartString.Size = new Size(68, 17);
            labelStartString.TabIndex = 25;
            labelStartString.Text = "起始字符串";
            toolTip.SetToolTip(labelStartString, "比如wav的RIFF");
            // 
            // textBoxEndString
            // 
            textBoxEndString.Enabled = false;
            textBoxEndString.Location = new Point(130, 215);
            textBoxEndString.Name = "textBoxEndString";
            textBoxEndString.Size = new Size(240, 23);
            textBoxEndString.TabIndex = 1;
            // 
            // labelEndString
            // 
            labelEndString.AutoSize = true;
            labelEndString.ForeColor = Color.FromArgb(255, 128, 0);
            labelEndString.Location = new Point(3, 220);
            labelEndString.Name = "labelEndString";
            labelEndString.Size = new Size(68, 17);
            labelEndString.TabIndex = 27;
            labelEndString.Text = "结束字符串";
            toolTip.SetToolTip(labelEndString, "比如wav的RIFF");
            // 
            // textBoxOutputFormat
            // 
            textBoxOutputFormat.Location = new Point(130, 5);
            textBoxOutputFormat.Name = "textBoxOutputFormat";
            textBoxOutputFormat.Size = new Size(240, 23);
            textBoxOutputFormat.TabIndex = 0;
            // 
            // labelOutputFormat
            // 
            labelOutputFormat.AutoSize = true;
            labelOutputFormat.ForeColor = Color.Black;
            labelOutputFormat.Location = new Point(3, 10);
            labelOutputFormat.Name = "labelOutputFormat";
            labelOutputFormat.Size = new Size(80, 17);
            labelOutputFormat.TabIndex = 17;
            labelOutputFormat.Text = "输出文件格式";
            toolTip.SetToolTip(labelOutputFormat, "输入要保存的文件后缀名，不需要输入点");
            // 
            // radioButtonOffsetString
            // 
            radioButtonOffsetString.AutoSize = true;
            radioButtonOffsetString.Location = new Point(3, 245);
            radioButtonOffsetString.Name = "radioButtonOffsetString";
            radioButtonOffsetString.Size = new Size(110, 21);
            radioButtonOffsetString.TabIndex = 28;
            radioButtonOffsetString.Text = "偏移验证字符串";
            toolTip.SetToolTip(radioButtonOffsetString, "比如wav的WAVEfmt、png的IHDR");
            radioButtonOffsetString.UseVisualStyleBackColor = true;
            radioButtonOffsetString.CheckedChanged += RadioButtonOffset_CheckedChanged;
            // 
            // radioButtonOffsetSequence
            // 
            radioButtonOffsetSequence.AutoSize = true;
            radioButtonOffsetSequence.Location = new Point(3, 270);
            radioButtonOffsetSequence.Name = "radioButtonOffsetSequence";
            radioButtonOffsetSequence.Size = new Size(122, 21);
            radioButtonOffsetSequence.TabIndex = 29;
            radioButtonOffsetSequence.Text = "偏移验证字节序列";
            toolTip.SetToolTip(radioButtonOffsetSequence, "比如png的49 48 44 52或者49484452");
            radioButtonOffsetSequence.UseVisualStyleBackColor = true;
            radioButtonOffsetSequence.CheckedChanged += RadioButtonOffset_CheckedChanged;
            // 
            // textBoxOffsetString
            // 
            textBoxOffsetString.Enabled = false;
            textBoxOffsetString.Location = new Point(131, 245);
            textBoxOffsetString.Name = "textBoxOffsetString";
            textBoxOffsetString.Size = new Size(239, 23);
            textBoxOffsetString.TabIndex = 30;
            // 
            // textBoxOffsetSequence
            // 
            textBoxOffsetSequence.Enabled = false;
            textBoxOffsetSequence.Location = new Point(130, 270);
            textBoxOffsetSequence.Name = "textBoxOffsetSequence";
            textBoxOffsetSequence.Size = new Size(240, 23);
            textBoxOffsetSequence.TabIndex = 32;
            // 
            // textBoxOffsetLength
            // 
            textBoxOffsetLength.Location = new Point(130, 295);
            textBoxOffsetLength.Name = "textBoxOffsetLength";
            textBoxOffsetLength.Size = new Size(80, 23);
            textBoxOffsetLength.TabIndex = 34;
            // 
            // labelOffsetLength
            // 
            labelOffsetLength.AutoSize = true;
            labelOffsetLength.ForeColor = Color.Green;
            labelOffsetLength.Location = new Point(3, 300);
            labelOffsetLength.Name = "labelOffsetLength";
            labelOffsetLength.Size = new Size(56, 17);
            labelOffsetLength.TabIndex = 35;
            labelOffsetLength.Text = "偏移数量";
            toolTip.SetToolTip(labelOffsetLength, "例如png的89504E47到IHDR的偏移为12字节");
            // 
            // textBoxTrimBytes
            // 
            textBoxTrimBytes.Location = new Point(130, 320);
            textBoxTrimBytes.Name = "textBoxTrimBytes";
            textBoxTrimBytes.Size = new Size(80, 23);
            textBoxTrimBytes.TabIndex = 36;
            textBoxTrimBytes.TextChanged += textBoxTrimBytes_TextChanged;
            // 
            // labelTrimBytes
            // 
            labelTrimBytes.AutoSize = true;
            labelTrimBytes.ForeColor = Color.Red;
            labelTrimBytes.Location = new Point(3, 325);
            labelTrimBytes.Name = "labelTrimBytes";
            labelTrimBytes.Size = new Size(104, 17);
            labelTrimBytes.TabIndex = 37;
            labelTrimBytes.Text = "排除文件尾字节数";
            toolTip.SetToolTip(labelTrimBytes, "有些游戏文件会在文件末尾处插入一些固定的垃圾字节，使用此功能可以移除，比如正当防卫4");
            // 
            // textBoxTrimRepeatedByte
            // 
            textBoxTrimRepeatedByte.Location = new Point(338, 320);
            textBoxTrimRepeatedByte.Name = "textBoxTrimRepeatedByte";
            textBoxTrimRepeatedByte.Size = new Size(32, 23);
            textBoxTrimRepeatedByte.TabIndex = 38;
            // 
            // labelTrimRepeatedByte
            // 
            labelTrimRepeatedByte.AutoSize = true;
            labelTrimRepeatedByte.ForeColor = Color.Red;
            labelTrimRepeatedByte.Location = new Point(216, 325);
            labelTrimRepeatedByte.Name = "labelTrimRepeatedByte";
            labelTrimRepeatedByte.Size = new Size(116, 17);
            labelTrimRepeatedByte.TabIndex = 39;
            labelTrimRepeatedByte.Text = "排除文件尾重复字节";
            toolTip.SetToolTip(labelTrimRepeatedByte, "有些游戏文件会在文件末尾处插入一些随机数量的重复垃圾字节，使用此功能可以移除，比如正当防卫4");
            // 
            // treeViewModes
            // 
            treeViewModes.Location = new Point(12, 41);
            treeViewModes.Name = "treeViewModes";
            treeViewModes.Size = new Size(265, 377);
            treeViewModes.TabIndex = 8;
            toolTip.SetToolTip(treeViewModes, "选择一种模式来提取你需要的文件");
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;
            // 
            // FileExtractor
            // 
            ClientSize = new Size(875, 757);
            Controls.Add(treeViewModes);
            Controls.Add(inputPanel);
            Controls.Add(buttonClearOutput);
            Controls.Add(buttonExtract);
            Controls.Add(richTextBoxOutput);
            Controls.Add(labelDirectoryPath);
            Controls.Add(textBoxDirectoryPath);
            ForeColor = Color.BlueViolet;
            MinimumSize = new Size(792, 551);
            Name = "FileExtractor";
            Text = "万能二进制提取器";
            inputPanel.ResumeLayout(false);
            inputPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox textBoxDirectoryPath;
        private System.Windows.Forms.Label labelDirectoryPath;
        private System.Windows.Forms.TextBox textBoxStartAddress;
        private System.Windows.Forms.Label labelStartAddress;
        private System.Windows.Forms.TextBox textBoxEndAddress;
        private System.Windows.Forms.Label labelEndAddress;
        private System.Windows.Forms.TextBox textBoxStartSequence;
        private System.Windows.Forms.Label labelStartSequence;
        private System.Windows.Forms.TextBox textBoxEndSequence;
        private System.Windows.Forms.Label labelEndSequence;
        private System.Windows.Forms.TextBox textBoxOutputFormat;
        private System.Windows.Forms.Label labelOutputFormat;
        private System.Windows.Forms.Button buttonExtract;
        private System.Windows.Forms.Button buttonClearOutput;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.TextBox textBoxHexAddress;
        private System.Windows.Forms.Label labelHexAddress;
        private System.Windows.Forms.Panel inputPanel;
    }
}
