using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalFileExtractor
{
    public partial class FileExtractor : Form
    {
        private TreeNode? selectedModeNode;

        public FileExtractor()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            InitializeTreeView();
            InitializeCollapsePanel();
            DisableInputBoxes();
            EnableDragDropSupport();
            InitializeOffsetControls();
            InitializeTrimControls();
            comboBoxTrimMode.SelectedIndexChanged += ComboBoxTrimMode_SelectedIndexChanged;
        }

        private void InitializeTreeView()
        {
            treeViewModes.Nodes.Clear();

            TreeNode mode1 = new TreeNode("模式1:指定字节序列提取");
            mode1.Tag = 1;

            TreeNode mode2 = new TreeNode("模式2:指定地址前提取");
            mode2.Tag = 2;

            TreeNode mode3 = new TreeNode("模式3:指定地址后提取");
            mode3.Tag = 3;

            TreeNode mode4 = new TreeNode("模式4:指定两个地址之间提取");
            mode4.Tag = 4;

            TreeNode mode5 = new TreeNode("模式5:指定字符串之间提取");
            mode5.Tag = 5;

            TreeNode mode6 = new TreeNode("模式6:无头有尾文件-字节序列模式");
            mode6.Tag = 6;

            TreeNode mode7 = new TreeNode("模式7:无头有尾文件-字符串模式");
            mode7.Tag = 7;

            treeViewModes.Nodes.Add(mode1);
            treeViewModes.Nodes.Add(mode2);
            treeViewModes.Nodes.Add(mode3);
            treeViewModes.Nodes.Add(mode4);
            treeViewModes.Nodes.Add(mode5);
            treeViewModes.Nodes.Add(mode6);
            treeViewModes.Nodes.Add(mode7);

            treeViewModes.AfterSelect += TreeViewModes_AfterSelect;
        }

        private void TreeViewModes_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            selectedModeNode = e.Node;
            if (selectedModeNode != null)
            {
                EnableInputBoxesBasedOnMode();
                if (!string.IsNullOrEmpty(textBoxDirectoryPath.Text) && Directory.Exists(textBoxDirectoryPath.Text))
                {
                    buttonExtract.Enabled = true;
                }
            }
        }

        private void InitializeOffsetControls()
        {
            radioButtonOffsetString.Checked = true;
            textBoxOffsetString.Enabled = true;
            textBoxOffsetSequence.Enabled = false;
        }

        private void InitializeTrimControls()
        {
            comboBoxTrimMode.SelectedIndex = 0;
            textBoxTrimBytes.Enabled = false;
            textBoxTrimRepeatedByte.Enabled = false;

            UpdateTrimControlsState();
        }
        private void UpdateTrimControlsState()
        {
            if (comboBoxTrimMode.SelectedIndex < 0) return;

            string selectedMode = comboBoxTrimMode.Text;

            switch (selectedMode)
            {
                case "无":
                    textBoxTrimBytes.Enabled = false;
                    textBoxTrimRepeatedByte.Enabled = false;
                    break;
                case "仅排除字节数":
                    textBoxTrimBytes.Enabled = true;
                    textBoxTrimRepeatedByte.Enabled = false;
                    break;
                case "仅排除重复字节":
                    textBoxTrimBytes.Enabled = false;
                    textBoxTrimRepeatedByte.Enabled = true;
                    break;
                case "两者都排除(字节数量优先)":
                case "两者都排除(重复字节优先)":
                    textBoxTrimBytes.Enabled = true;
                    textBoxTrimRepeatedByte.Enabled = true;
                    break;
            }
        }
        private void ComboBoxTrimMode_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateTrimControlsState();
        }
        private void RadioButtonOffset_CheckedChanged(object? sender, EventArgs e)
        {
            if (radioButtonOffsetString.Checked)
            {
                textBoxOffsetString.Enabled = true;
                textBoxOffsetSequence.Enabled = false;
            }
            else if (radioButtonOffsetSequence.Checked)
            {
                textBoxOffsetString.Enabled = false;
                textBoxOffsetSequence.Enabled = true;
            }
        }

        private void InitializeCollapsePanel()
        {
            DisableAllInputControls();
        }

        private void DisableAllInputControls()
        {
            foreach (Control control in inputPanel.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Enabled = false;
                    textBox.Visible = false;
                }
                else if (control is Label label)
                {
                    label.Visible = false;
                }
                else if (control is RadioButton radio)
                {
                    radio.Enabled = false;
                    radio.Visible = false;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.Enabled = false;
                    comboBox.Visible = false;
                }
            }
        }

        private void EnableInputBoxesBasedOnMode()
        {
            if (selectedModeNode == null) return;

            DisableAllInputControls();

            int mode = (int)selectedModeNode.Tag;
            switch (mode)
            {
                case 1:
                    ShowControlsForMode1();
                    break;
                case 2:
                    ShowControlsForMode2();
                    break;
                case 3:
                    ShowControlsForMode3();
                    break;
                case 4:
                    ShowControlsForMode4();
                    break;
                case 5:
                    ShowControlsForMode5();
                    break;
                case 6:
                    ShowControlsForMode6();
                    break;
                case 7:
                    ShowControlsForMode7();
                    break;
            }

            textBoxOutputFormat.Enabled = true;
            textBoxOutputFormat.Visible = true;
            labelOutputFormat.Visible = true;
        }

        private void ShowControlsForMode1()
        {
            textBoxStartSequence.Enabled = true;
            textBoxStartSequence.Visible = true;
            labelStartSequence.Visible = true;

            textBoxEndSequence.Enabled = true;
            textBoxEndSequence.Visible = true;
            labelEndSequence.Visible = true;

            ShowCommonControls();
        }

        private void ShowControlsForMode2()
        {
            textBoxHexAddress.Enabled = true;
            textBoxHexAddress.Visible = true;
            labelHexAddress.Visible = true;

            textBoxStartSequence.Enabled = true;
            textBoxStartSequence.Visible = true;
            labelStartSequence.Visible = true;

            textBoxEndSequence.Enabled = true;
            textBoxEndSequence.Visible = true;
            labelEndSequence.Visible = true;

            ShowCommonControls();
        }

        private void ShowControlsForMode3()
        {
            textBoxHexAddress.Enabled = true;
            textBoxHexAddress.Visible = true;
            labelHexAddress.Visible = true;

            textBoxStartSequence.Enabled = true;
            textBoxStartSequence.Visible = true;
            labelStartSequence.Visible = true;

            textBoxEndSequence.Enabled = true;
            textBoxEndSequence.Visible = true;
            labelEndSequence.Visible = true;

            ShowCommonControls();
        }

        private void ShowControlsForMode4()
        {
            textBoxStartAddress.Enabled = true;
            textBoxStartAddress.Visible = true;
            labelStartAddress.Visible = true;

            textBoxEndAddress.Enabled = true;
            textBoxEndAddress.Visible = true;
            labelEndAddress.Visible = true;

            textBoxStartSequence.Enabled = true;
            textBoxStartSequence.Visible = true;
            labelStartSequence.Visible = true;

            textBoxEndSequence.Enabled = true;
            textBoxEndSequence.Visible = true;
            labelEndSequence.Visible = true;

            ShowCommonControls();
        }

        private void ShowControlsForMode5()
        {
            textBoxStartString.Enabled = true;
            textBoxStartString.Visible = true;
            labelStartString.Visible = true;

            textBoxEndString.Enabled = true;
            textBoxEndString.Visible = true;
            labelEndString.Visible = true;

            ShowCommonControls();
        }

        private void ShowControlsForMode6()
        {
            textBoxEndSequence.Enabled = true;
            textBoxEndSequence.Visible = true;
            labelEndSequence.Visible = true;

            textBoxStartSequence.Enabled = false;
            textBoxStartSequence.Visible = false;
            labelStartSequence.Visible = false;

            ShowMode6And7Controls();
        }

        private void ShowControlsForMode7()
        {
            textBoxEndString.Enabled = true;
            textBoxEndString.Visible = true;
            labelEndString.Visible = true;

            textBoxStartString.Enabled = false;
            textBoxStartString.Visible = false;
            labelStartString.Visible = false;

            ShowMode6And7Controls();
        }

        private void ShowMode6And7Controls()
        {
            radioButtonOffsetString.Enabled = false;
            radioButtonOffsetString.Visible = false;
            radioButtonOffsetSequence.Enabled = false;
            radioButtonOffsetSequence.Visible = false;
            textBoxOffsetString.Enabled = false;
            textBoxOffsetString.Visible = false;
            textBoxOffsetSequence.Enabled = false;
            textBoxOffsetSequence.Visible = false;
            textBoxOffsetLength.Enabled = false;
            textBoxOffsetLength.Visible = false;
            labelOffsetLength.Visible = false;

            comboBoxTrimMode.Enabled = false;
            comboBoxTrimMode.Visible = false;
            labelTrimMode.Visible = false;
            textBoxTrimBytes.Enabled = false;
            textBoxTrimBytes.Visible = false;
            labelTrimBytes.Visible = false;
            textBoxTrimRepeatedByte.Enabled = false;
            textBoxTrimRepeatedByte.Visible = false;
            labelTrimRepeatedByte.Visible = false;

            textBoxOutputFormat.Enabled = true;
            textBoxOutputFormat.Visible = true;
            labelOutputFormat.Visible = true;
        }

        private void ShowCommonControls()
        {
            radioButtonOffsetString.Enabled = true;
            radioButtonOffsetString.Visible = true;
            radioButtonOffsetSequence.Enabled = true;
            radioButtonOffsetSequence.Visible = true;
            textBoxOffsetString.Enabled = radioButtonOffsetString.Checked;
            textBoxOffsetString.Visible = true;
            textBoxOffsetSequence.Enabled = radioButtonOffsetSequence.Checked;
            textBoxOffsetSequence.Visible = true;
            textBoxOffsetLength.Enabled = true;
            textBoxOffsetLength.Visible = true;
            labelOffsetLength.Visible = true;

            textBoxTrimBytes.Enabled = false;
            textBoxTrimBytes.Visible = true;
            labelTrimBytes.Visible = true;
            textBoxTrimRepeatedByte.Enabled = false;
            textBoxTrimRepeatedByte.Visible = true;
            labelTrimRepeatedByte.Visible = true;
            comboBoxTrimMode.Enabled = true;
            comboBoxTrimMode.Visible = true;
            labelTrimMode.Visible = true;

            textBoxOutputFormat.Enabled = true;
            textBoxOutputFormat.Visible = true;
            labelOutputFormat.Visible = true;
            UpdateTrimControlsState();
        }

        private void EnableDragDropSupport()
        {
            textBoxDirectoryPath.AllowDrop = true;
            textBoxDirectoryPath.DragEnter += TextBoxDirectoryPath_DragEnter;
            textBoxDirectoryPath.DragDrop += TextBoxDirectoryPath_DragDrop;
            textBoxDirectoryPath.DragLeave += TextBoxDirectoryPath_DragLeave;

            this.AllowDrop = true;
            this.DragEnter += Form_DragEnter;
            this.DragDrop += Form_DragDrop;
        }

        private void TextBoxDirectoryPath_DragEnter(object? sender, DragEventArgs e)
        {
            if (textBoxDirectoryPath == null) return;

            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (files != null && files.Length == 1 && Directory.Exists(files[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                    textBoxDirectoryPath.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TextBoxDirectoryPath_DragDrop(object? sender, DragEventArgs e)
        {
            if (textBoxDirectoryPath == null) return;

            textBoxDirectoryPath.BackColor = System.Drawing.SystemColors.Window;

            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (files != null && files.Length == 1 && Directory.Exists(files[0]))
                {
                    textBoxDirectoryPath.Text = files[0];
                    AppendToOutput($"已通过拖放选择文件夹:{files[0]}");

                    if (!string.IsNullOrEmpty(textBoxDirectoryPath.Text) && Directory.Exists(textBoxDirectoryPath.Text))
                    {
                        treeViewModes.Enabled = true;
                        if (selectedModeNode != null)
                        {
                            buttonExtract.Enabled = true;
                        }
                    }
                }
                else
                {
                    AppendToOutput("错误:请拖放单个文件夹");
                }
            }
        }

        private void TextBoxDirectoryPath_DragLeave(object? sender, EventArgs e)
        {
            if (textBoxDirectoryPath == null) return;

            textBoxDirectoryPath.BackColor = System.Drawing.SystemColors.Window;
        }

        private void Form_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (files != null && files.Length == 1 && Directory.Exists(files[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (files != null && files.Length == 1 && Directory.Exists(files[0]))
                {
                    textBoxDirectoryPath.Text = files[0];
                    AppendToOutput($"已通过窗体拖放选择文件夹:{files[0]}");

                    if (!string.IsNullOrEmpty(textBoxDirectoryPath.Text) && Directory.Exists(textBoxDirectoryPath.Text))
                    {
                        treeViewModes.Enabled = true;
                        if (selectedModeNode != null)
                        {
                            buttonExtract.Enabled = true;
                        }
                    }
                }
                else
                {
                    AppendToOutput("错误:请拖放单个文件夹到路径文本框或窗体");
                }
            }
        }

        private void AppendToOutput(string message)
        {
            if (richTextBoxOutput.InvokeRequired)
            {
                richTextBoxOutput.Invoke(new Action<string>(AppendToOutput), message);
                return;
            }

            richTextBoxOutput.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
            richTextBoxOutput.ScrollToCaret();
        }

        private void DisableInputBoxes()
        {
            treeViewModes.Enabled = false;
            buttonExtract.Enabled = false;
        }

        private int GetSelectedMode()
        {
            if (selectedModeNode == null) return 0;
            return (int)selectedModeNode.Tag;
        }

        private void textBoxDirectoryPath_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDirectoryPath.Text) && Directory.Exists(textBoxDirectoryPath.Text))
            {
                treeViewModes.Enabled = true;
                if (selectedModeNode != null)
                {
                    buttonExtract.Enabled = true;
                }
            }
            else
            {
                DisableInputBoxes();
            }
        }

        private async void buttonExtract_Click(object sender, EventArgs e)
        {
            try
            {
                string directoryPath = textBoxDirectoryPath.Text;
                if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
                {
                    MessageBox.Show($"错误:{directoryPath}不是一个有效的目录。");
                    return;
                }

                if (selectedModeNode == null)
                {
                    MessageBox.Show("请选择提取模式");
                    return;
                }

                buttonExtract.Enabled = false;
                richTextBoxOutput.AppendText("开始提取...\n");

                string extractMode = "";
                string? startAddress = null;
                string? endAddress = null;
                string? startString = null;
                string? endString = null;

                int selectedMode = GetSelectedMode();
                switch (selectedMode)
                {
                    case 1:
                        extractMode = "all";
                        break;
                    case 2:
                        extractMode = "before";
                        if (textBoxHexAddress != null)
                        {
                            string hexAddress2 = textBoxHexAddress.Text;
                            if (!string.IsNullOrEmpty(hexAddress2))
                            {
                                startAddress = hexAddress2;
                            }
                        }
                        break;
                    case 3:
                        extractMode = "after";
                        if (textBoxHexAddress != null)
                        {
                            string hexAddress3 = textBoxHexAddress.Text;
                            if (!string.IsNullOrEmpty(hexAddress3))
                            {
                                startAddress = hexAddress3;
                            }
                        }
                        break;
                    case 4:
                        extractMode = "between";
                        startAddress = textBoxStartAddress.Text;
                        endAddress = textBoxEndAddress.Text;
                        break;
                    case 5:
                        extractMode = "string_between";
                        startString = textBoxStartString.Text;
                        endString = textBoxEndString.Text;
                        break;
                    case 6:
                        extractMode = "end_sequence_only";
                        break;
                    case 7:
                        extractMode = "end_string_only";
                        endString = textBoxEndString.Text;
                        break;
                    default:
                        break;
                }

                byte[] startSequenceBytes = Array.Empty<byte>();
                if (selectedMode != 5 && selectedMode != 6 && selectedMode != 7)
                {
                    string startSequenceInput = textBoxStartSequence.Text;
                    if (string.IsNullOrEmpty(startSequenceInput))
                    {
                        MessageBox.Show("起始字节序列为必填项，请输入。");
                        buttonExtract.Enabled = true;
                        return;
                    }

                    try
                    {
                        startSequenceBytes = ParseStartSequence(startSequenceInput);
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show($"起始字节序列格式错误:{ex.Message}\n\n请输入有效的十六进制字节序列，例如:\n- 89 50 4E 47\n- 89504E47\n- 00*4(表示4个0x00字节)");
                        buttonExtract.Enabled = true;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"解析起始字节序列时发生错误:{ex.Message}");
                        buttonExtract.Enabled = true;
                        return;
                    }
                }

                byte[]? endSequenceBytes = null;
                if (selectedMode != 5 && selectedMode != 7)
                {
                    string endSequenceInput = textBoxEndSequence.Text;
                    if (selectedMode == 6 && string.IsNullOrEmpty(endSequenceInput))
                    {
                        MessageBox.Show("模式6需要结束字节序列，请输入。");
                        buttonExtract.Enabled = true;
                        return;
                    }

                    if (!string.IsNullOrEmpty(endSequenceInput))
                    {
                        try
                        {
                            endSequenceBytes = ParseEndSequence(endSequenceInput);
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show($"结束字节序列格式错误:{ex.Message}\n\n请输入有效的十六进制字节序列");
                            buttonExtract.Enabled = true;
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"解析结束字节序列时发生错误:{ex.Message}");
                            buttonExtract.Enabled = true;
                            return;
                        }
                    }
                }
                else if (selectedMode == 5 || selectedMode == 7)
                {
                    if (selectedMode == 5 && string.IsNullOrEmpty(startString))
                    {
                        MessageBox.Show("模式5需要起始字符串，请输入。");
                        buttonExtract.Enabled = true;
                        return;
                    }
                    else if (selectedMode == 7 && string.IsNullOrEmpty(endString))
                    {
                        MessageBox.Show("模式7需要结束字符串，请输入。");
                        buttonExtract.Enabled = true;
                        return;
                    }
                }

                string? offsetString = null;
                byte[]? offsetSequence = null;
                int offsetLength = 0;

                if (selectedMode >= 1 && selectedMode <= 5)
                {
                    if (!string.IsNullOrEmpty(textBoxOffsetLength.Text))
                    {
                        if (!int.TryParse(textBoxOffsetLength.Text, out offsetLength) || offsetLength < 0)
                        {
                            MessageBox.Show("偏移数量必须是一个有效的非负整数");
                            buttonExtract.Enabled = true;
                            return;
                        }

                        if (radioButtonOffsetString.Checked && !string.IsNullOrEmpty(textBoxOffsetString.Text))
                        {
                            offsetString = textBoxOffsetString.Text;
                        }
                        else if (radioButtonOffsetSequence.Checked && !string.IsNullOrEmpty(textBoxOffsetSequence.Text))
                        {
                            try
                            {
                                offsetSequence = ParseStartSequence(textBoxOffsetSequence.Text);
                            }
                            catch (FormatException ex)
                            {
                                MessageBox.Show($"偏移序列格式错误:{ex.Message}");
                                buttonExtract.Enabled = true;
                                return;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"解析偏移序列时发生错误:{ex.Message}");
                                buttonExtract.Enabled = true;
                                return;
                            }
                        }
                    }
                }

                int trimBytes = 0;
                byte? trimRepeatedByte = null;
                string trimMode = "none";

                if (selectedMode >= 1 && selectedMode <= 5)
                {
                    if (!string.IsNullOrEmpty(textBoxTrimBytes.Text))
                    {
                        if (!int.TryParse(textBoxTrimBytes.Text, out trimBytes) || trimBytes < 0)
                        {
                            MessageBox.Show("排除文件尾字节数必须是一个有效的非负整数");
                            buttonExtract.Enabled = true;
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(textBoxTrimRepeatedByte.Text))
                    {
                        string trimByteText = textBoxTrimRepeatedByte.Text.Trim().ToUpper();
                        if (trimByteText.Length == 2 && IsHex(trimByteText))
                        {
                            try
                            {
                                trimRepeatedByte = Convert.ToByte(trimByteText, 16);
                            }
                            catch (FormatException)
                            {
                                MessageBox.Show("排除文件尾重复字节必须是有效的十六进制字节(00-FF)");
                                buttonExtract.Enabled = true;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("排除文件尾重复字节格式错误，请输入两位十六进制数(如:00, FF, 1A)");
                            buttonExtract.Enabled = true;
                            return;
                        }
                    }

                    if (comboBoxTrimMode.SelectedIndex >= 0)
                    {
                        switch (comboBoxTrimMode.SelectedIndex)
                        {
                            case 0:
                                trimMode = "none";
                                break;
                            case 1:
                                trimMode = "bytes_only";
                                break;
                            case 2:
                                trimMode = "repeated_only";
                                break;
                            case 3:
                                trimMode = "both_bytes_first";
                                break;
                            case 4:
                                trimMode = "both_repeated_first";
                                break;
                        }
                    }
                }

                string outputFormat = textBoxOutputFormat.Text;
                if (string.IsNullOrEmpty(outputFormat))
                {
                    MessageBox.Show("输出文件格式为必填项，请输入。");
                    buttonExtract.Enabled = true;
                    return;
                }

                if (outputFormat.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
                {
                    MessageBox.Show("输出文件格式包含非法字符");
                    buttonExtract.Enabled = true;
                    return;
                }

                int totalExtractedFiles = 0;
                int processedFiles = 0;

                try
                {
                    string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
                    totalExtractedFiles = 0;

                    await Task.Run(() =>
                    {
                        foreach (string file in files)
                        {
                            try
                            {
                                int extractedCount = ExtractContent(
                                    file,
                                    startSequenceBytes,
                                    endSequenceBytes,
                                    outputFormat,
                                    extractMode,
                                    startAddress,
                                    endAddress,
                                    startString,
                                    endString,
                                    offsetString,
                                    offsetSequence,
                                    offsetLength,
                                    trimBytes,
                                    trimRepeatedByte,
                                    trimMode,
                                    (message) =>
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            richTextBoxOutput.AppendText(message);
                                        });
                                    });

                                totalExtractedFiles += extractedCount;
                                processedFiles++;

                                this.Invoke((MethodInvoker)delegate
                                {
                                    richTextBoxOutput.AppendText($"已处理文件{processedFiles}/{files.Length}\n");
                                });
                            }
                            catch (Exception ex)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    richTextBoxOutput.AppendText($"处理文件{file}时出错:{ex.Message}\n");
                                });
                            }
                        }
                    });

                    richTextBoxOutput.AppendText($"\n提取完成!总共从{processedFiles}个文件中提取了{totalExtractedFiles}个文件。\n");
                }
                catch (Exception ex)
                {
                    richTextBoxOutput.AppendText($"提取过程中出错:{ex.Message}\n");
                    MessageBox.Show($"提取过程中发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"发生未预期的错误:\n{ex.Message}\n\n堆栈跟踪:\n{ex.StackTrace}";
                richTextBoxOutput.AppendText($"{errorMessage}\n");
                MessageBox.Show(errorMessage, "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonExtract.Enabled = true;
            }
        }

        static bool IsHex(string text)
        {
            foreach (char c in text)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F')))
                    return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBoxOutput.Text = "";
        }

        private void buttonClearOutput_Click(object sender, EventArgs e)
        {
            richTextBoxOutput.Clear();
            AppendToOutput("输出信息已清空");
        }

        private void TextBoxDirectoryPath_Enter(object sender, EventArgs e)
        {
            if (textBoxDirectoryPath.Text == "拖放文件夹到此或点击选择文件夹...")
            {
                textBoxDirectoryPath.Text = "";
                textBoxDirectoryPath.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void TextBoxDirectoryPath_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDirectoryPath.Text))
            {
                textBoxDirectoryPath.Text = "拖放文件夹到此或点击选择文件夹...";
                textBoxDirectoryPath.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        static byte[] ParseStartSequence(string startSequenceInput)
        {
            if (string.IsNullOrEmpty(startSequenceInput))
            {
                return Array.Empty<byte>();
            }

            if (startSequenceInput.Contains('*'))
            {
                string[] parts = startSequenceInput.Split('*');
                byte byteValue = Convert.ToByte(parts[0].Replace(" ", ""), 16);
                int repeatCount = int.Parse(parts[1]);
                byte[] result = new byte[repeatCount];
                for (int i = 0; i < repeatCount; i++)
                {
                    result[i] = byteValue;
                }
                return result;
            }
            else
            {
                return StringToByteArray(startSequenceInput.Replace(" ", ""));
            }
        }

        static byte[] ParseEndSequence(string endSequenceInput)
        {
            return ParseStartSequence(endSequenceInput);
        }

        static long FindEndIndex(MemoryMappedViewAccessor accessor, long startIndex, byte[]? endSequence, byte[] startSequenceBytes, long fileSize)
        {
            if (endSequence == null || endSequence.Length == 0)
            {
                long nextStartIndex = IndexOfSequence(accessor, startSequenceBytes, startIndex + startSequenceBytes.Length, fileSize);
                return nextStartIndex == -1 ? -1 : nextStartIndex;
            }
            else
            {
                long endIndex = IndexOfSequence(accessor, endSequence, startIndex + 1, fileSize);

                if (endIndex == -1)
                {
                    return -1;
                }

                if (startSequenceBytes.SequenceEqual(endSequence))
                {
                    return endIndex;
                }
                else
                {
                    return endIndex + endSequence.Length;
                }
            }
        }

        static int ExtractContent(string filePath, byte[] startSequenceBytes, byte[]? endSequence = null, string outputFormat = "bin",
                           string extractMode = "all", string? startAddress = null, string? endAddress = null,
                           string? startString = null, string? endString = null,
                           string? offsetString = null, byte[]? offsetSequence = null, int offsetLength = 0,
                           int trimBytes = 0, byte? trimRepeatedByte = null, string trimMode = "none",
                           Action<string>? progressCallback = null)
        {
            outputFormat = outputFormat ?? "bin";
            int extractedCount = 0;

            try
            {
                var fileInfo = new FileInfo(filePath);
                long fileSize = fileInfo.Length;
                long startRange = 0;
                long endRange = fileSize;
                string? directoryName = Path.GetDirectoryName(filePath);
                if (directoryName == null)
                {
                    progressCallback?.Invoke($"无法获取文件目录:{filePath}\n");
                    return 0;
                }

                string extractedDirectory = Path.Combine(directoryName, "Extracted");
                if (!Directory.Exists(extractedDirectory))
                {
                    Directory.CreateDirectory(extractedDirectory);
                }

                if (extractMode == "end_sequence_only")
                {
                    if (endSequence == null || endSequence.Length == 0)
                    {
                        progressCallback?.Invoke("结束字节序列不能为空\n");
                        return 0;
                    }

                    using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read))
                    using (var accessor = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read))
                    {
                        long lastEndIndex = 0;
                        long currentEndIndex = 0;

                        currentEndIndex = IndexOfSequence(accessor, endSequence, lastEndIndex, fileSize);
                        if (currentEndIndex == -1)
                        {
                            progressCallback?.Invoke($"在{filePath}中未找到结束字节序列\n");
                            return 0;
                        }

                        do
                        {
                            long extractStart = lastEndIndex;
                            long extractEnd = currentEndIndex + endSequence.Length;
                            long extractLength = extractEnd - extractStart;

                            if (extractLength > 0)
                            {
                                byte[] extractedData = new byte[extractLength];
                                for (long i = 0; i < extractLength; i++)
                                {
                                    extractedData[i] = accessor.ReadByte(extractStart + i);
                                }

                                extractedData = TrimFileEnd(extractedData, 0, null, "none");

                                string baseFileName = Path.GetFileNameWithoutExtension(filePath);
                                string newFilename = $"{baseFileName}_endseq_{extractedCount + 1}.{outputFormat}";
                                string newFilePath = Path.Combine(extractedDirectory, newFilename);

                                File.WriteAllBytes(newFilePath, extractedData);

                                progressCallback?.Invoke($"提取的内容保存为:{newFilePath}(长度:{extractedData.Length}字节)\n");
                                extractedCount++;
                            }

                            lastEndIndex = extractEnd;
                            currentEndIndex = IndexOfSequence(accessor, endSequence, lastEndIndex, fileSize);

                        } while (currentEndIndex != -1 && lastEndIndex < fileSize);
                    }
                    return extractedCount;
                }

                if (extractMode == "end_string_only")
                {
                    if (string.IsNullOrEmpty(endString))
                    {
                        progressCallback?.Invoke("结束字符串不能为空\n");
                        return 0;
                    }

                    byte[] endStringBytes = Encoding.UTF8.GetBytes(endString);

                    using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read))
                    using (var accessor = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read))
                    {
                        long lastEndIndex = 0;
                        long currentEndIndex = 0;

                        currentEndIndex = IndexOfSequence(accessor, endStringBytes, lastEndIndex, fileSize);
                        if (currentEndIndex == -1)
                        {
                            progressCallback?.Invoke($"在{filePath}中未找到结束字符串\n");
                            return 0;
                        }

                        do
                        {
                            long extractStart = lastEndIndex;
                            long extractEnd = currentEndIndex + endStringBytes.Length;
                            long extractLength = extractEnd - extractStart;

                            if (extractLength > 0)
                            {
                                byte[] extractedData = new byte[extractLength];
                                for (long i = 0; i < extractLength; i++)
                                {
                                    extractedData[i] = accessor.ReadByte(extractStart + i);
                                }

                                extractedData = TrimFileEnd(extractedData, 0, null, "none");

                                string baseFileName = Path.GetFileNameWithoutExtension(filePath);
                                string newFilename = $"{baseFileName}_endstr_{extractedCount + 1}.{outputFormat}";
                                string newFilePath = Path.Combine(extractedDirectory, newFilename);

                                File.WriteAllBytes(newFilePath, extractedData);

                                progressCallback?.Invoke($"提取的内容保存为:{newFilePath}(长度:{extractedData.Length}字节)\n");
                                extractedCount++;
                            }

                            lastEndIndex = extractEnd;
                            currentEndIndex = IndexOfSequence(accessor, endStringBytes, lastEndIndex, fileSize);

                        } while (currentEndIndex != -1 && lastEndIndex < fileSize);
                    }
                    return extractedCount;
                }

                if (extractMode == "string_between")
                {
                    if (string.IsNullOrEmpty(startString))
                    {
                        progressCallback?.Invoke("起始字符串不能为空\n");
                        return 0;
                    }

                    byte[] startStringBytes = Encoding.UTF8.GetBytes(startString);
                    byte[]? endStringBytes = null;
                    if (!string.IsNullOrEmpty(endString))
                    {
                        endStringBytes = Encoding.UTF8.GetBytes(endString);
                    }

                    using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read))
                    using (var accessor = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read))
                    {
                        long startIndexInContent = 0;
                        while (startIndexInContent < fileSize)
                        {
                            startIndexInContent = IndexOfSequence(accessor, startStringBytes, startIndexInContent, fileSize);
                            if (startIndexInContent == -1)
                            {
                                if (extractedCount == 0)
                                {
                                    progressCallback?.Invoke($"在{filePath}中未找到起始字符串\n");
                                }
                                else
                                {
                                    progressCallback?.Invoke($"在{filePath}中未找到更多起始字符串\n");
                                }
                                break;
                            }

                            long endIndexInContent;

                            if (endStringBytes != null)
                            {
                                endIndexInContent = IndexOfSequence(accessor, endStringBytes, startIndexInContent + startStringBytes.Length, fileSize);
                                if (endIndexInContent == -1)
                                {
                                    progressCallback?.Invoke($"在{filePath}中未找到对应的结束字符串，跳过\n");
                                    startIndexInContent += startStringBytes.Length;
                                    continue;
                                }
                                else
                                {
                                    endIndexInContent += endStringBytes.Length;
                                }
                            }
                            else
                            {
                                endIndexInContent = IndexOfSequence(accessor, startStringBytes, startIndexInContent + startStringBytes.Length, fileSize);
                                if (endIndexInContent == -1)
                                {
                                    progressCallback?.Invoke($"在{filePath}中未找到下一个起始字符串，跳过\n");
                                    startIndexInContent += startStringBytes.Length;
                                    continue;
                                }
                            }

                            long extractLength = endIndexInContent - startIndexInContent;

                            if (extractLength <= 0)
                            {
                                progressCallback?.Invoke($"警告:提取长度为0，跳过\n");
                                startIndexInContent += startStringBytes.Length;
                                continue;
                            }

                            byte[] extractedData = new byte[extractLength];
                            for (long i = 0; i < extractLength; i++)
                            {
                                extractedData[i] = accessor.ReadByte(startIndexInContent + i);
                            }

                            extractedData = TrimFileEnd(extractedData, trimBytes, trimRepeatedByte, trimMode);

                            string baseFileName = Path.GetFileNameWithoutExtension(filePath);
                            string newFilename = $"{baseFileName}_str_{extractedCount + 1}.{outputFormat}";
                            string newFilePath = Path.Combine(extractedDirectory, newFilename);

                            File.WriteAllBytes(newFilePath, extractedData);

                            progressCallback?.Invoke($"提取的内容保存为:{newFilePath}(长度:{extractedData.Length}字节)\n");
                            extractedCount++;

                            startIndexInContent = endIndexInContent;
                        }
                    }
                    return extractedCount;
                }

                if (startAddress != null && endAddress != null)
                {
                    long startIndex = Convert.ToInt64(startAddress.Replace("0x", ""), 16);
                    long endIndex = Convert.ToInt64(endAddress.Replace("0x", ""), 16);
                    if (startIndex > fileSize || endIndex > fileSize || startIndex > endIndex)
                    {
                        progressCallback?.Invoke($"指定地址范围{startAddress}-{endAddress}无效，无法提取。\n");
                        return 0;
                    }
                    startRange = startIndex;
                    endRange = endIndex;
                }
                else if (startAddress != null)
                {
                    long targetIndex = Convert.ToInt64(startAddress.Replace("0x", ""), 16);
                    if (targetIndex > fileSize)
                    {
                        progressCallback?.Invoke($"指定地址{startAddress}超出文件范围，无法提取。\n");
                        return 0;
                    }
                    if (extractMode == "before")
                    {
                        startRange = 0;
                        endRange = targetIndex;
                    }
                    else if (extractMode == "after")
                    {
                        startRange = targetIndex;
                        endRange = fileSize;
                    }
                    else
                    {
                        progressCallback?.Invoke("无效的提取模式参数\n");
                        return 0;
                    }
                }

                using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read))
                using (var accessor = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read))
                {
                    long startIndexInContent = startRange;
                    while (startIndexInContent < endRange)
                    {
                        startIndexInContent = IndexOfSequence(accessor, startSequenceBytes, startIndexInContent, endRange);
                        if (startIndexInContent == -1)
                        {
                            if (extractedCount == 0)
                            {
                                progressCallback?.Invoke($"在{filePath}中未找到起始序列\n");
                            }
                            else
                            {
                                progressCallback?.Invoke($"在{filePath}中未找到更多起始序列\n");
                            }
                            break;
                        }

                        if (offsetLength > 0 && (offsetString != null || offsetSequence != null))
                        {
                            byte[]? offsetBytes = offsetSequence;
                            if (offsetString != null)
                            {
                                offsetBytes = Encoding.UTF8.GetBytes(offsetString);
                            }

                            if (offsetBytes != null)
                            {
                                long offsetPosition = startIndexInContent + offsetLength;
                                if (offsetPosition + offsetBytes.Length <= fileSize)
                                {
                                    bool offsetMatch = true;
                                    for (int i = 0; i < offsetBytes.Length; i++)
                                    {
                                        if (accessor.ReadByte(offsetPosition + i) != offsetBytes[i])
                                        {
                                            offsetMatch = false;
                                            break;
                                        }
                                    }

                                    if (!offsetMatch)
                                    {
                                        progressCallback?.Invoke($"偏移验证失败，跳过该位置\n");
                                        startIndexInContent += startSequenceBytes.Length;
                                        continue;
                                    }
                                }
                            }
                        }

                        long endIndexInContent = FindEndIndex(accessor, startIndexInContent, endSequence, startSequenceBytes, endRange);
                        if (endIndexInContent == -1)
                        {
                            progressCallback?.Invoke($"在{filePath}中未找到对应的结束序列，跳过\n");
                            startIndexInContent += startSequenceBytes.Length;
                            continue;
                        }

                        endIndexInContent = Math.Min(endIndexInContent, endRange);

                        long extractLength = endIndexInContent - startIndexInContent;

                        byte[] extractedData = new byte[extractLength];
                        for (long i = 0; i < extractLength; i++)
                        {
                            extractedData[i] = accessor.ReadByte(startIndexInContent + i);
                        }

                        extractedData = TrimFileEnd(extractedData, trimBytes, trimRepeatedByte, trimMode);

                        string baseFileName = Path.GetFileNameWithoutExtension(filePath);
                        string newFilename = $"{baseFileName}_{extractedCount + 1}.{outputFormat}";
                        string newFilePath = Path.Combine(extractedDirectory, newFilename);
                        File.WriteAllBytes(newFilePath, extractedData);

                        progressCallback?.Invoke($"提取的内容保存为:{newFilePath}(长度:{extractedData.Length}字节)\n");
                        extractedCount++;

                        startIndexInContent = endIndexInContent;
                    }
                }
            }
            catch (Exception ex)
            {
                progressCallback?.Invoke($"处理文件{filePath}时出错，错误信息:{ex}\n");
            }

            return extractedCount;
        }

        static byte[] TrimFileEnd(byte[] data, int trimBytes, byte? trimRepeatedByte, string trimMode)
        {
            if (data.Length == 0) return data;

            byte[] result = (byte[])data.Clone();

            switch (trimMode)
            {
                case "none":
                    break;

                case "bytes_only":
                    if (trimBytes > 0 && trimBytes < result.Length)
                    {
                        byte[] temp = new byte[result.Length - trimBytes];
                        Array.Copy(result, 0, temp, 0, temp.Length);
                        result = temp;
                    }
                    break;

                case "repeated_only":
                    if (trimRepeatedByte.HasValue && result.Length > 0)
                    {
                        int trimCount = 0;
                        for (int i = result.Length - 1; i >= 0; i--)
                        {
                            if (result[i] == trimRepeatedByte.Value)
                            {
                                trimCount++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (trimCount > 0)
                        {
                            byte[] temp = new byte[result.Length - trimCount];
                            Array.Copy(result, 0, temp, 0, temp.Length);
                            result = temp;
                        }
                    }
                    break;

                case "both_bytes_first":
                    if (trimBytes > 0 && trimBytes < result.Length)
                    {
                        byte[] temp = new byte[result.Length - trimBytes];
                        Array.Copy(result, 0, temp, 0, temp.Length);
                        result = temp;
                    }

                    if (trimRepeatedByte.HasValue && result.Length > 0)
                    {
                        int trimCount = 0;
                        for (int i = result.Length - 1; i >= 0; i--)
                        {
                            if (result[i] == trimRepeatedByte.Value)
                            {
                                trimCount++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (trimCount > 0)
                        {
                            byte[] temp = new byte[result.Length - trimCount];
                            Array.Copy(result, 0, temp, 0, temp.Length);
                            result = temp;
                        }
                    }
                    break;

                case "both_repeated_first":
                    if (trimRepeatedByte.HasValue && result.Length > 0)
                    {
                        int trimCount = 0;
                        for (int i = result.Length - 1; i >= 0; i--)
                        {
                            if (result[i] == trimRepeatedByte.Value)
                            {
                                trimCount++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (trimCount > 0)
                        {
                            byte[] temp = new byte[result.Length - trimCount];
                            Array.Copy(result, 0, temp, 0, temp.Length);
                            result = temp;
                        }
                    }

                    if (trimBytes > 0 && trimBytes < result.Length)
                    {
                        byte[] temp = new byte[result.Length - trimBytes];
                        Array.Copy(result, 0, temp, 0, temp.Length);
                        result = temp;
                    }
                    break;
            }

            return result;
        }

        static long IndexOfSequence(MemoryMappedViewAccessor accessor, byte[] sequence, long startOffset, long maxOffset)
        {
            long maxSearchPosition = maxOffset - sequence.Length;
            if (startOffset > maxSearchPosition)
                return -1;

            byte firstByte = sequence[0];
            for (long i = startOffset; i <= maxSearchPosition; i++)
            {
                byte currentByte = accessor.ReadByte(i);
                if (currentByte == firstByte)
                {
                    bool match = true;
                    for (int j = 1; j < sequence.Length; j++)
                    {
                        if (accessor.ReadByte(i + j) != sequence[j])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        static byte[] StringToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return Array.Empty<byte>();

            hex = hex.Replace(" ", "").Replace("0x", "").Replace("0X", "");

            if (hex.Length % 2 != 0)
            {
                throw new FormatException("十六进制字符串长度必须为偶数");
            }

            foreach (char c in hex)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')))
                {
                    throw new FormatException($"包含无效的十六进制字符:'{c}'");
                }
            }

            try
            {
                int length = hex.Length;
                byte[] bytes = new byte[length / 2];
                for (int i = 0; i < length; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }
                return bytes;
            }
            catch (FormatException ex)
            {
                throw new FormatException($"无效的十六进制格式:{hex}", ex);
            }
        }

        private void textBoxTrimBytes_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelHexAddress_Click(object sender, EventArgs e)
        {

        }
    }
}
