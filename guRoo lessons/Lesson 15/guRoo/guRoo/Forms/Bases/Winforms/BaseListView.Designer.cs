namespace guRoo.Forms.Base
{
    partial class BaseListView
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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            buttonCheckAll = new Button();
            buttonCancel = new Button();
            buttonUncheckAll = new Button();
            textBoxFilter = new TextBox();
            listView = new ListView();
            buttonOK = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(textBoxFilter, 0, 0);
            tableLayoutPanel1.Controls.Add(listView, 0, 1);
            tableLayoutPanel1.Controls.Add(buttonOK, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel1.Size = new Size(415, 486);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(buttonCheckAll, 1, 0);
            tableLayoutPanel2.Controls.Add(buttonCancel, 0, 0);
            tableLayoutPanel2.Controls.Add(buttonUncheckAll, 2, 0);
            tableLayoutPanel2.Location = new System.Drawing.Point(14, 395);
            tableLayoutPanel2.Margin = new Padding(14, 3, 14, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(387, 36);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // buttonCheckAll
            // 
            buttonCheckAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonCheckAll.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCheckAll.Location = new System.Drawing.Point(131, 0);
            buttonCheckAll.Margin = new Padding(3, 0, 3, 0);
            buttonCheckAll.Name = "buttonCheckAll";
            buttonCheckAll.Size = new Size(122, 36);
            buttonCheckAll.TabIndex = 2;
            buttonCheckAll.Text = "Check all";
            buttonCheckAll.UseVisualStyleBackColor = true;
            buttonCheckAll.Click += buttonCheckAll_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCancel.Location = new System.Drawing.Point(0, 0);
            buttonCancel.Margin = new Padding(0, 0, 3, 0);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(125, 36);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonUncheckAll
            // 
            buttonUncheckAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonUncheckAll.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonUncheckAll.Location = new System.Drawing.Point(259, 0);
            buttonUncheckAll.Margin = new Padding(3, 0, 0, 0);
            buttonUncheckAll.Name = "buttonUncheckAll";
            buttonUncheckAll.Size = new Size(128, 36);
            buttonUncheckAll.TabIndex = 0;
            buttonUncheckAll.Text = "Uncheck all";
            buttonUncheckAll.UseVisualStyleBackColor = true;
            buttonUncheckAll.Click += buttonUncheckAll_Click;
            // 
            // textBoxFilter
            // 
            textBoxFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxFilter.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxFilter.Location = new System.Drawing.Point(14, 14);
            textBoxFilter.Margin = new Padding(14, 14, 14, 3);
            textBoxFilter.Name = "textBoxFilter";
            textBoxFilter.Size = new Size(387, 25);
            textBoxFilter.TabIndex = 1;
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView.CheckBoxes = true;
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.Location = new System.Drawing.Point(14, 42);
            listView.Margin = new Padding(14, 3, 14, 3);
            listView.Name = "listView";
            listView.Size = new Size(387, 347);
            listView.TabIndex = 2;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = System.Windows.Forms.View.Details;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonOK.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonOK.Location = new System.Drawing.Point(14, 437);
            buttonOK.Margin = new Padding(14, 3, 14, 14);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(387, 35);
            buttonOK.TabIndex = 3;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // BaseListView
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 486);
            Controls.Add(tableLayoutPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(431, 525);
            Name = "BaseListView";
            Text = "Select objects from list";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox textBoxFilter;
        private Button buttonCheckAll;
        private Button buttonCancel;
        private Button buttonUncheckAll;
        private ListView listView;
        private Button buttonOK;
    }
}