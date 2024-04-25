﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Linq.Expressions;
namespace лб1тфияк
{
    public partial class Form1 : Form
    {
        private int fileCounter = 1;
        private string openedFilePath;
        private Lexer lexer;
  
        public Form1()
        {
            InitializeComponent();
            lexer = new Lexer();
         
            
        }



       

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Users\Ольга\source\repos\лб1тфияк\лб1тфияк\bin\Debug\netcoreapp3.1";
            try
            {
                string fileName = $"File{fileCounter}.txt";
                string filePath = Path.Combine(folderPath, fileName);
                // Создание файла
                File.WriteAllText(filePath, $"Содержимое файла {fileCounter}");
                MessageBox.Show($"Файл {fileName} успешно создан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                fileCounter++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CheckForChanges()
        {
            if (richTextBox1.Modified)
            {
                DialogResult result = MessageBox.Show("Хотите сохранить изменения?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveChanges();
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckForChanges())
            {
                return;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        // Считываем содержимое файла
                        string fileContent = File.ReadAllText(filePath);

                        // Устанавливаем текст в RichTextBox
                        richTextBox1.Text = fileContent;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

               
   

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(openedFilePath))
            {
 
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        openedFilePath = saveFileDialog.FileName;
                    }
                    else
                    {
                        return; // Пользователь отменил сохранение
                    }
                }
            }

            try
            {
                // Сохраняем содержимое RichTextBox в тот же файл
                File.WriteAllText(openedFilePath, richTextBox1.Text);
                MessageBox.Show("Файл успешно сохранен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            richTextBox1.Modified = false;
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.SelectedText = "";
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.SelectedText = Clipboard.GetText();
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Users\Ольга\source\repos\лб1тфияк\лб1тфияк\bin\Debug\netcoreapp3.1";
            try
            {
                string fileName = $"File{fileCounter}.txt";
                string filePath = Path.Combine(folderPath, fileName);
                // Создание файла
                File.WriteAllText(filePath, $"Содержимое файла {fileCounter}");
                MessageBox.Show($"Файл {fileName} успешно создан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                fileCounter++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!CheckForChanges())
            {
                return;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    openedFilePath = openFileDialog.FileName;

                    try
                    {
                        // Считываем содержимое файла
                        string fileContent = File.ReadAllText(openedFilePath);

                        // Устанавливаем текст в RichTextBox
                        richTextBox1.Text = fileContent;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(openedFilePath))
                {
                    // Сохраняем содержимое RichTextBox в тот же файл
                    File.WriteAllText(openedFilePath, richTextBox1.Text);
                    MessageBox.Show("Файл успешно сохранен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Если openedFilePath не установлена, используйте диалог сохранения
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        openedFilePath = saveFileDialog.FileName;
                        // Сохраняем содержимое RichTextBox в новый файл
                        File.WriteAllText(openedFilePath, richTextBox1.Text);
                        MessageBox.Show("Файл успешно сохранен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            richTextBox1.Modified = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

            if (richTextBox1.SelectionLength > 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.SelectedText = Clipboard.GetText();
            }
        }
        private bool IsRichTextBoxModified()
        {
            return richTextBox1.Modified;
        }

        private void PromptToSaveChanges()
        {
            if (IsRichTextBoxModified())
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Предупреждение", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveChanges();
                    Close();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.No)
                {
                    Close();
                }
            }
        }

        private void SaveChanges()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                richTextBox1.Modified = false;
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRichTextBoxModified())
            {
                PromptToSaveChanges();
            }
            else
            {
                Close();
            }
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Information()
        {
            const string filePath = @"C:\Users\Ольга\source\repos\лб1тфияк\лб1тфияк\bin\Debug\netcoreapp3.1\y.html";
            if (System.IO.File.Exists(filePath))
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
            }
        }
        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Information();
          

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Information();
        }
        private void About_program()
        {
            const string filePath = @"C:\Users\Ольга\source\repos\лб1тфияк\лб1тфияк\bin\Debug\netcoreapp3.1\about.html";
            if (System.IO.File.Exists(filePath))
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_program();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            About_program();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsRichTextBoxModified())
            {
                PromptToSaveChanges();
            }
            else
            {
                Close();
            }

        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string expression = richTextBox1.Text;
            try
            {
                List<string> poliz = ConvertToPoliz(expression);
                richTextBox2.Text = "Poliz: " + string.Join(" ", poliz) + Environment.NewLine;

                Parser parser = new Parser(expression);
                double result = parser.Parse();
                richTextBox2.AppendText($"Result: {result}");
            }
            catch (Exception ex)
            {
                richTextBox2.Text = $"Error: {ex.Message}";
            }
        }

        private List<string> ConvertToPoliz(string expression)
        {
            List<string> poliz = new List<string>();
            Stack<char> operators = new Stack<char>();
            bool lastTokenWasOperatorOrOpeningBracket = true; // Flag to handle unary minus

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (char.IsDigit(c))
                {
                    StringBuilder numBuilder = new StringBuilder();
                    while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                    {
                        numBuilder.Append(expression[i]);
                        i++;
                    }
                    i--;
                    poliz.Add(numBuilder.ToString());
                    lastTokenWasOperatorOrOpeningBracket = false;
                }
                else if (c == '(')
                {
                    operators.Push(c);
                    lastTokenWasOperatorOrOpeningBracket = true;
                }
                else if (c == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        poliz.Add(operators.Pop().ToString());
                    }
                    if (operators.Count == 0)
                    {
                        throw new Exception($"Unmatched closing parenthesis at position {i}");
                    }
                    operators.Pop(); // Pop '('
                    lastTokenWasOperatorOrOpeningBracket = false;
                }
                else if (c == '+' || c == '*' || c == '/')
                {
                    while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(c))
                    {
                        poliz.Add(operators.Pop().ToString());
                    }
                    operators.Push(c);
                    lastTokenWasOperatorOrOpeningBracket = true;
                }
                else if (c == '-')
                {
                    // Check if it's a unary minus or a binary minus
                    if (lastTokenWasOperatorOrOpeningBracket)
                    {
                        // Unary minus
                        poliz.Add("0"); // Add a zero placeholder
                        operators.Push(c);
                    }
                    else
                    {
                        // Binary minus
                        while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(c))
                        {
                            poliz.Add(operators.Pop().ToString());
                        }
                        operators.Push(c);
                    }
                    lastTokenWasOperatorOrOpeningBracket = true;
                }
                else
                {
                    throw new Exception($"Invalid character '{c}' at position {i+1}");
                }
            }

            while (operators.Count > 0)
            {
                poliz.Add(operators.Pop().ToString());
            }

            return poliz;
        }

        private int Priority(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
           
        }
    }
}
    