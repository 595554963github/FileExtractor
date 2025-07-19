using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalFileExtractor
{
    public partial class FileExtractor : Form
    {
        public FileExtractor()
        {
            InitializeComponent();
            if (textBoxHexAddress != null)
            {
                textBoxHexAddress.Enabled = false;
            }
            DisableInputBoxes();
        }

        private void DisableInputBoxes()
        {
            radioButtonMode1.Enabled = false;
            radioButtonMode2.Enabled = false;
            radioButtonMode3.Enabled = false;
            radioButtonMode4.Enabled = false;
            textBoxStartSequence.Enabled = false;
            textBoxEndSequence.Enabled = false;
            textBoxMinRepeatCount.Enabled = false;
            textBoxStartAddress.Enabled = false;
            textBoxEndAddress.Enabled = false;
            textBoxOutputFormat.Enabled = false;
            buttonExtract.Enabled = false;
            if (textBoxHexAddress != null)
            {
                textBoxHexAddress.Enabled = false;
            }
        }

        private void EnableModeSelection()
        {
            radioButtonMode1.Enabled = true;
            radioButtonMode2.Enabled = true;
            radioButtonMode3.Enabled = true;
            radioButtonMode4.Enabled = true;
        }

        private void EnableInputBoxesBasedOnMode()
        {
            switch (GetSelectedMode())
            {
                case 1:
                    if (textBoxHexAddress != null)
                    {
                        textBoxHexAddress.Enabled = false;
                    }
                    textBoxStartAddress.Enabled = false;
                    textBoxEndAddress.Enabled = false;
                    textBoxStartSequence.Enabled = true;
                    textBoxEndSequence.Enabled = true;
                    break;
                case 2:
                    if (textBoxHexAddress != null)
                    {
                        textBoxHexAddress.Enabled = true;
                    }
                    textBoxStartAddress.Enabled = false;
                    textBoxEndAddress.Enabled = false;
                    textBoxStartSequence.Enabled = true;
                    textBoxEndSequence.Enabled = true;
                    break;
                case 3:
                    if (textBoxHexAddress != null)
                    {
                        textBoxHexAddress.Enabled = true;
                    }
                    textBoxStartAddress.Enabled = false;
                    textBoxEndAddress.Enabled = false;
                    textBoxStartSequence.Enabled = true;
                    textBoxEndSequence.Enabled = true;
                    break;
                case 4:
                    if (textBoxHexAddress != null)
                    {
                        textBoxHexAddress.Enabled = false;
                    }
                    textBoxStartAddress.Enabled = true;
                    textBoxEndAddress.Enabled = true;
                    textBoxStartSequence.Enabled = true;
                    textBoxEndSequence.Enabled = true;
                    break;
                default:
                    break;
            }
            textBoxOutputFormat.Enabled = true;
            buttonExtract.Enabled = true;
        }

        private int GetSelectedMode()
        {
            if (radioButtonMode1.Checked) return 1;
            if (radioButtonMode2.Checked) return 2;
            if (radioButtonMode3.Checked) return 3;
            if (radioButtonMode4.Checked) return 4;
            return 0;
        }

        private void textBoxDirectoryPath_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDirectoryPath.Text) && Directory.Exists(textBoxDirectoryPath.Text))
            {
                EnableModeSelection();
            }
            else
            {
                DisableInputBoxes();
            }
        }

        private void radioButtonMode1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMode1.Checked)
            {
                EnableInputBoxesBasedOnMode();
            }
        }

        private void radioButtonMode2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMode2.Checked)
            {
                EnableInputBoxesBasedOnMode();
            }
        }

        private void radioButtonMode3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMode3.Checked)
            {
                EnableInputBoxesBasedOnMode();
            }
        }

        private void radioButtonMode4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMode4.Checked)
            {
                EnableInputBoxesBasedOnMode();
            }
        }

        private void textBoxEndSequence_TextChanged(object sender, EventArgs e)
        {
            string endSequenceInput = textBoxEndSequence.Text;
            if (!string.IsNullOrEmpty(endSequenceInput))
            {
                byte[] endSequenceBytes = ParseEndSequence(endSequenceInput);
                if (GetSelectedMode() != 1 && endSequenceBytes.Length == 1 && !endSequenceInput.Contains('*'))
                {
                    textBoxMinRepeatCount.Enabled = true;
                }
                else
                {
                    textBoxMinRepeatCount.Enabled = false;
                }
            }
            else
            {
                textBoxMinRepeatCount.Enabled = false;
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
            if (string.IsNullOrEmpty(endSequenceInput))
            {
                return Array.Empty<byte>();
            }

            string[] parts = endSequenceInput.Split(' ');
            List<byte> result = new List<byte>();
            foreach (string part in parts)
            {
                if (part.Contains('*'))
                {
                    string[] subParts = part.Split('*');
                    byte byteValue = Convert.ToByte(subParts[0].Replace(" ", ""), 16);
                    int repeatCount = int.Parse(subParts[1]);
                    for (int i = 0; i < repeatCount; i++)
                    {
                        result.Add(byteValue);
                    }
                }
                else
                {
                    result.Add(Convert.ToByte(part.Replace(" ", ""), 16));
                }
            }
            return result.ToArray();
        }

        static long FindEndIndex(MemoryMappedViewAccessor accessor, long startIndex, byte[]? endSequence, int minRepeatCount, byte[] startSequenceBytes, long fileSize)
        {
            if (endSequence == null || endSequence.Length == 0)
            {
                long nextStartIndex = IndexOfSequence(accessor, startSequenceBytes, startIndex + 1, fileSize);
                return nextStartIndex == -1 ? fileSize : nextStartIndex;
            }
            else
            {
                if (minRepeatCount == 0)
                {
                    long endIndex = IndexOfSequence(accessor, endSequence, startIndex + 1, fileSize);
                    return endIndex == -1 ? fileSize : endIndex + endSequence.Length;
                }
                else
                {
                    byte byteValue = endSequence[0];
                    int repeatCount = 0;
                    long currentIndex = startIndex + 1;
                    while (currentIndex < fileSize)
                    {
                        byte currentByte = accessor.ReadByte(currentIndex);
                        if (currentByte == byteValue)
                        {
                            repeatCount++;
                            if (repeatCount >= minRepeatCount)
                            {
                                if (minRepeatCount == 0 ||
                                    (currentIndex + 1 < fileSize && accessor.ReadByte(currentIndex + 1) != byteValue))
                                {
                                    return currentIndex + 1;
                                }
                            }
                        }
                        else
                        {
                            repeatCount = 0;
                        }
                        currentIndex++;
                    }
                    return fileSize;
                }
            }
        }

        static int ExtractContent(string filePath, byte[] startSequenceBytes, byte[]? endSequence = null, string outputFormat = "bin",
                                   string extractMode = "all", string? startAddress = null, string? endAddress = null, int minRepeatCount = 0, Action<string>? progressCallback = null)
        {
            outputFormat = outputFormat ?? "bin";
            int extractedCount = 0;

            try
            {
                var fileInfo = new FileInfo(filePath);
                long fileSize = fileInfo.Length;
                long startRange = 0;
                long endRange = fileSize;

                if (startAddress != null && endAddress != null)
                {
                    long startIndex = Convert.ToInt64(startAddress.Replace("0x", ""), 16);
                    long endIndex = Convert.ToInt64(endAddress.Replace("0x", ""), 16);
                    if (startIndex > fileSize || endIndex > fileSize || startIndex > endIndex)
                    {
                        progressCallback?.Invoke($"指定地址范围 {startAddress}-{endAddress} 无效，无法提取。\n");
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
                        progressCallback?.Invoke($"指定地址 {startAddress} 超出文件范围，无法提取。\n");
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
                            progressCallback?.Invoke($"在 {filePath} 中未找到更多起始序列\n");
                            break;
                        }

                        long endIndexInContent = FindEndIndex(accessor, startIndexInContent, endSequence, minRepeatCount, startSequenceBytes, endRange);
                        endIndexInContent = Math.Min(endIndexInContent, endRange);

                        string newFilename = $"{Path.GetFileNameWithoutExtension(filePath)}_{extractedCount}.{outputFormat}";
                        string directoryName = Path.GetDirectoryName(filePath) ?? ".";
                        string newFilePath = Path.Combine(directoryName, newFilename);

                        ExtractAndSaveSegment(filePath, newFilePath, startIndexInContent, endIndexInContent - startIndexInContent);

                        progressCallback?.Invoke($"提取的内容保存为: {newFilePath}\n");
                        extractedCount++;
                        startIndexInContent = endIndexInContent;
                    }
                }
            }
            catch (Exception e)
            {
                progressCallback?.Invoke($"处理文件 {filePath} 时出错，错误信息：{e}\n");
            }

            return extractedCount;
        }

        static void ExtractAndSaveSegment(string sourcePath, string destPath, long startPosition, long length)
        {
            const int bufferSize = 1024 * 1024; // 1MB buffer
            var buffer = new byte[bufferSize];

            using (var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan))
            using (var destStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize))
            {
                sourceStream.Seek(startPosition, SeekOrigin.Begin);

                long bytesRemaining = length;
                while (bytesRemaining > 0)
                {
                    int bytesToRead = (int)Math.Min(bufferSize, bytesRemaining);
                    int bytesRead = sourceStream.Read(buffer, 0, bytesToRead);
                    if (bytesRead == 0) break;

                    destStream.Write(buffer, 0, bytesRead);
                    bytesRemaining -= bytesRead;
                }
            }
        }

        static long IndexOfSequence(MemoryMappedViewAccessor accessor, byte[] sequence, long startOffset, long maxOffset)
        {
            long maxSearchPosition = maxOffset - sequence.Length;
            if (startOffset > maxSearchPosition)
                return -1;

            byte firstByte = sequence[0];
            byte[] buffer = new byte[sequence.Length];

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
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private async void buttonExtract_Click(object sender, EventArgs e)
        {
            string directoryPath = textBoxDirectoryPath.Text;
            if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                MessageBox.Show($"错误: {directoryPath} 不是一个有效的目录。");
                return;
            }

            buttonExtract.Enabled = false;
            richTextBoxOutput.AppendText("开始提取...\n");

            string extractMode = "";
            string? startAddress = null;
            string? endAddress = null;
            switch (GetSelectedMode())
            {
                case 1:
                    extractMode = "all";
                    break;
                case 2:
                    extractMode = "before";
                    string hexAddress2 = "";
                    if (textBoxHexAddress != null)
                    {
                        hexAddress2 = textBoxHexAddress.Text;
                    }
                    if (!string.IsNullOrEmpty(hexAddress2))
                    {
                        startAddress = hexAddress2;
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
                default:
                    break;
            }

            string startSequenceInput = textBoxStartSequence.Text;
            if (string.IsNullOrEmpty(startSequenceInput))
            {
                MessageBox.Show("起始字节序列为必填项，请输入。");
                buttonExtract.Enabled = true;
                return;
            }
            byte[] startSequenceBytes = ParseStartSequence(startSequenceInput);

            string endSequenceInput = textBoxEndSequence.Text;
            int minRepeatCount = 0;
            byte[]? endSequenceBytes = null;
            if (!string.IsNullOrEmpty(endSequenceInput))
            {
                endSequenceBytes = ParseEndSequence(endSequenceInput);
                if (!endSequenceInput.Contains('*') && endSequenceBytes.Length == 1 && int.TryParse(textBoxMinRepeatCount.Text, out minRepeatCount))
                {
                    // 成功解析
                }
            }

            string outputFormat = textBoxOutputFormat.Text;
            if (string.IsNullOrEmpty(outputFormat))
            {
                MessageBox.Show("输出文件格式为必填项，请输入。");
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
                        int extractedCount = ExtractContent(
                            file,
                            startSequenceBytes,
                            endSequenceBytes,
                            outputFormat,
                            extractMode,
                            startAddress,
                            endAddress,
                            minRepeatCount,
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
                });

                richTextBoxOutput.AppendText($"\n提取完成！总共从{processedFiles}个文件中提取了{totalExtractedFiles}个文件。\n");
            }
            catch (Exception ex)
            {
                richTextBoxOutput.AppendText($"提取过程中出错: {ex.Message}\n");
            }
            finally
            {
                buttonExtract.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBoxOutput.Text = "";
        }

        private System.Windows.Forms.TextBox? textBoxHexAddress;
        private System.Windows.Forms.Label? labelHexAddress;
        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxDirectoryPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}
