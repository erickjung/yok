// Yok 
// https://github.com/erickjung/yok
//
// Copyright (c) 2012-2015 Erick Jung
//
// This code is distributed under the terms and conditions of the MIT license.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Drawing;
using System.Windows.Forms;

namespace yok.App.Forms
{
    /// <summary>
    ///     Input dialog
    /// </summary>
    public class InputBoxDialog : Form
    {
        private Button _btnCancel;
        private Button _btnOk;
        private string _defaultValue = string.Empty;
        private string _formCaption = string.Empty;
        private string _formPrompt = string.Empty;
        private string _inputResponse = string.Empty;
        private Label _prompt;
        private TextBox _txtInput;

        public InputBoxDialog()
        {
            InitializeComponent();
        }

        public string FormCaption
        {
            get { return _formCaption; }
            set { _formCaption = value; }
        } // property FormCaption

        public string FormPrompt
        {
            get { return _formPrompt; }
            set { _formPrompt = value; }
        } // property FormPrompt

        public string InputResponse
        {
            get { return _inputResponse; }
            set { _inputResponse = value; }
        } // property InputResponse

        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        } // property DefaultValue

        private void InitializeComponent()
        {
            _prompt = new Label();
            _txtInput = new TextBox();
            _btnOk = new Button();
            _btnCancel = new Button();

            SuspendLayout();

            // 
            // lblPrompt
            // 
            _prompt.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
            _prompt.BackColor = SystemColors.Control;
            _prompt.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _prompt.Location = new Point(12, 9);
            _prompt.Name = "_prompt";
            _prompt.Size = new Size(302, 82);
            _prompt.TabIndex = 3;
            // 
            // txtInput
            // 
            _txtInput.Location = new Point(8, 100);
            _txtInput.Name = "_txtInput";
            _txtInput.PasswordChar = '*';
            _txtInput.Size = new Size(379, 20);
            _txtInput.TabIndex = 0;
            // 
            // btnOK
            // 
            _btnOk.Location = new Point(320, 10);
            _btnOk.Name = "_btnOk";
            _btnOk.Size = new Size(75, 25);
            _btnOk.TabIndex = 4;
            _btnOk.Text = "&OK";
            _btnOk.UseVisualStyleBackColor = true;
            _btnOk.Click += BtnOkClick;
            // 
            // btnCancel
            // 
            _btnCancel.DialogResult = DialogResult.Cancel;
            _btnCancel.Location = new Point(320, 41);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(75, 25);
            _btnCancel.TabIndex = 5;
            _btnCancel.Text = "&Cancel";
            _btnCancel.UseVisualStyleBackColor = true;
            _btnCancel.Click += BtnCancelClick;
            // 
            // InputBoxDialog
            // 
            AcceptButton = _btnOk;
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = SystemColors.Control;
            CancelButton = _btnCancel;
            ClientSize = new Size(398, 128);
            Controls.Add(_btnCancel);
            Controls.Add(_btnOk);
            Controls.Add(_txtInput);
            Controls.Add(_prompt);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputBoxDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InputBox";
            Load += InputBoxLoad;
            ResumeLayout(false);
            PerformLayout();
        }

        private void InputBoxLoad(object sender, EventArgs e)
        {
            _txtInput.Text = _defaultValue;
            _prompt.Text = _formPrompt;
            Text = _formCaption;
            _txtInput.SelectionStart = 0;
            _txtInput.SelectionLength = _txtInput.Text.Length;
            _txtInput.Focus();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            InputResponse = _txtInput.Text;
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}