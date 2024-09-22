using FileSharing.Components;
using FileSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileSharing.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private bool _isInserting = false;
        
        public HomeView()
        {
            InitializeComponent();

           
            editor.ScrollToVerticalOffset(editor.VerticalOffset);
        
            editor.TextChanged += AssignedDoc;

            HttpSelectedState.HttpDataChanged += AssigneSelectedChange;
        }

        private void editor_TextChanged(object sender, KeyEventArgs e)
        {
            if (!(sender is RichTextBox textBox))
                return;
            //syntaxt highligher
            Brush bracketColor = Brushes.MediumBlue; 
            Brush openBracketColor = Brushes.Orange;
            Brush quoteColor = Brushes.Green;

            bool isShiftPressed = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

            TextPointer newCaretPosition = null;
            if (e.Key == Key.Enter)
            {
             
                e.Handled = true;
                textBox.CaretPosition.InsertTextInRun("\n");
            }
            switch (e.Key)
            {
                case Key.OemOpenBrackets:
                    if (!isShiftPressed)
                        newCaretPosition = InsertPairedCharacter(textBox, '[', ']', bracketColor);
                    else
                        newCaretPosition = InsertPairedCharacter(textBox, '{', '}', openBracketColor);
                    break;

                case Key.OemQuotes:
                    if (!isShiftPressed)
                        newCaretPosition = InsertPairedCharacter(textBox, '\'', '\'', quoteColor);
                    else
                        newCaretPosition = InsertPairedCharacter(textBox, '"', '"', quoteColor);
                    break;

             

                default:
                    return;
            }
            e.Handled = true;
            if (newCaretPosition != null)
            {
                textBox.CaretPosition = newCaretPosition;
                textBox.Selection.Select(newCaretPosition, newCaretPosition);
            }

        }

        private TextPointer InsertPairedCharacter(RichTextBox textBox, char openChar, char closeChar,Brush syntaxColor)
        {
            if (textBox == null || textBox.Selection == null)
                return null;

           


            textBox.BeginChange();

            try
            {
                TextPointer caretPosition = textBox.Selection.Start;

                if (caretPosition != null)
                {
                    // Insert opening character with color
                    TextRange openCharRange = new TextRange(caretPosition, caretPosition.GetInsertionPosition(LogicalDirection.Forward));
                    openCharRange.Text = openChar.ToString();
                    openCharRange.ApplyPropertyValue(TextElement.ForegroundProperty, syntaxColor);

                    // Move to next position
                    caretPosition = openCharRange.End;

                    // Insert closing character with the same color
                    TextRange closeCharRange = new TextRange(caretPosition, caretPosition.GetInsertionPosition(LogicalDirection.Forward));
                    closeCharRange.Text = closeChar.ToString();
                    closeCharRange.ApplyPropertyValue(TextElement.ForegroundProperty, syntaxColor);

                    // Return the position between the brackets
                    return closeCharRange.Start;
                }
            }
            finally
            {
                textBox.EndChange();
            }

            return null;
        }
     
        private void AssignedDoc(object sender, TextChangedEventArgs e)
        {
            TextRange textRange = new TextRange(editor.Document.ContentStart, editor.Document.ContentEnd);
            HomeViewModel.FlowDoc = textRange.Text;
           
        }

        private void editor_LineBehaviour(object sender, KeyEventArgs e)
        {
            var richTextBox = sender as RichTextBox;

            if (richTextBox == null) return;

            if (e.Key == Key.Enter)
            {
                e.Handled = true;  // Prevent the default action

                var caretPosition = richTextBox.CaretPosition;

                if (caretPosition != null)
                {
                    // Insert a LineBreak at the current caret position
                    caretPosition.InsertTextInRun("\n");
                
                    // Move the caret to the new line
                    var newCaretPosition = caretPosition.GetPositionAtOffset(1, LogicalDirection.Forward);

                    if (newCaretPosition != null)
                    {
                        richTextBox.CaretPosition = newCaretPosition;
                    }
                    else
                    {
                        // Fall back to setting the caret to the end of the document if necessary
                        richTextBox.CaretPosition = richTextBox.Document.ContentEnd;
                    }
                }
                UpdateLineNumbers(richTextBox);
            }
            else if (e.Key == Key.Back)
            {
               
                // Handle backspace if needed
                // This section can be expanded if special handling for backspace is required
            }
        }

        private void UpdateLineNumbers(RichTextBox richTextBox)
        {
            if (richTextBox == null) return; // Check if the RichTextBox is valid

            // Get the entire text without trimming to preserve empty lines
            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            string[] lines = textRange.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None); // Keep all lines, including blank ones
            int lineCount = lines.Length;

            // Update the ListBox
            LineCountBox.Items.Clear();
            for (int i = 1; i+1 <= lineCount; i++)
            {
                LineCountBox.Items.Add(i.ToString());
            }
            var totalLines = LineCountBox.Items.Count;
            if (totalLines > 0)
            {
                // Use a safe item index within bounds
                int itemIndex = Math.Min((int)richTextBox.VerticalOffset, totalLines - 1);
                LineCountBox.ScrollIntoView(LineCountBox.Items[itemIndex]);
            }
        }

        private void editor_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0) // Handle only vertical scrolling
            {
                var verticalOffset = editor.VerticalOffset;
                var totalLines = LineCountBox.Items.Count;

                // Check if verticalOffset is within the range of LineCountBox items
                if (totalLines > 0)
                {
                    int itemIndex = (int)verticalOffset;
                    if (itemIndex >= 0 && itemIndex < totalLines)
                    {
                        LineCountBox.ScrollIntoView(LineCountBox.Items[itemIndex]);
                    }
                }
            }
        }
        
        private void AssigneSelectedChange()
        {
            Dispatcher.Invoke(() =>
            {
                // Clear the RichTextBox content
                editor.Document.Blocks.Clear();

                // Assign the HttpBody content to the RichTextBox
                editor.AppendText(HttpSelectedState.HttpBody);
            });

        }
    }
}
