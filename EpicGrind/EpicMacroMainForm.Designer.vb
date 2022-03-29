<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EpicMacroMainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EpicMacroMainForm))
        Me.FileTable = New System.Windows.Forms.DataGridView()
        Me.Fails = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MacroCommandBox = New System.Windows.Forms.RichTextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.MacroTable = New System.Windows.Forms.DataGridView()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.ShowNews = New System.Windows.Forms.CheckBox()
        Me.CountMessage = New System.Windows.Forms.Label()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.CheckBox_LayoutLoop = New System.Windows.Forms.CheckBox()
        Me.CheckBox_SDI = New System.Windows.Forms.CheckBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.MacroDescription = New System.Windows.Forms.RichTextBox()
        CType(Me.FileTable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MacroTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FileTable
        '
        Me.FileTable.AllowUserToAddRows = False
        Me.FileTable.AllowUserToDeleteRows = False
        Me.FileTable.AllowUserToResizeColumns = False
        Me.FileTable.AllowUserToResizeRows = False
        Me.FileTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.FileTable.ColumnHeadersVisible = False
        Me.FileTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Fails, Me.Column1})
        Me.FileTable.Location = New System.Drawing.Point(4, 21)
        Me.FileTable.Name = "FileTable"
        Me.FileTable.ReadOnly = True
        Me.FileTable.RowHeadersVisible = False
        Me.FileTable.RowHeadersWidth = 5
        Me.FileTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.FileTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.FileTable.Size = New System.Drawing.Size(210, 251)
        Me.FileTable.TabIndex = 0
        '
        'Fails
        '
        Me.Fails.HeaderText = "Fails"
        Me.Fails.Name = "Fails"
        Me.Fails.ReadOnly = True
        Me.Fails.Width = 210
        '
        'Column1
        '
        Me.Column1.HeaderText = "Index"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'MacroCommandBox
        '
        Me.MacroCommandBox.Location = New System.Drawing.Point(355, 21)
        Me.MacroCommandBox.Name = "MacroCommandBox"
        Me.MacroCommandBox.Size = New System.Drawing.Size(192, 251)
        Me.MacroCommandBox.TabIndex = 3
        Me.MacroCommandBox.Text = ""
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(486, 49)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(62, 23)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Izpildīt"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(418, 50)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(62, 23)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Atcelt"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(421, 2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(62, 23)
        Me.Button5.TabIndex = 7
        Me.Button5.Text = "Formatēt"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'MacroTable
        '
        Me.MacroTable.AllowUserToAddRows = False
        Me.MacroTable.AllowUserToDeleteRows = False
        Me.MacroTable.AllowUserToResizeColumns = False
        Me.MacroTable.AllowUserToResizeRows = False
        Me.MacroTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MacroTable.ColumnHeadersVisible = False
        Me.MacroTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.Column3})
        Me.MacroTable.Location = New System.Drawing.Point(220, 21)
        Me.MacroTable.MultiSelect = False
        Me.MacroTable.Name = "MacroTable"
        Me.MacroTable.ReadOnly = True
        Me.MacroTable.RowHeadersVisible = False
        Me.MacroTable.RowHeadersWidth = 5
        Me.MacroTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.MacroTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MacroTable.Size = New System.Drawing.Size(130, 251)
        Me.MacroTable.TabIndex = 8
        '
        'Column2
        '
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 130
        '
        'Column3
        '
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(486, 2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(62, 23)
        Me.Button6.TabIndex = 9
        Me.Button6.Text = "Saglabāt"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(356, 2)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(62, 23)
        Me.Button7.TabIndex = 10
        Me.Button7.Text = "Jauns Macro"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(288, 2)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(63, 23)
        Me.Button8.TabIndex = 11
        Me.Button8.Text = "Dzēst"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Pievienotie DWG faili:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(217, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Saglabātie Macro:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(352, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Izvēlētais Macro:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.Label4.Location = New System.Drawing.Point(50, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(164, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Edgars M. © Daina EL 2021"
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(221, 2)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(63, 23)
        Me.Button10.TabIndex = 18
        Me.Button10.Text = "Mape"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button13)
        Me.Panel1.Controls.Add(Me.Button12)
        Me.Panel1.Controls.Add(Me.ShowNews)
        Me.Panel1.Controls.Add(Me.CountMessage)
        Me.Panel1.Controls.Add(Me.Button11)
        Me.Panel1.Controls.Add(Me.Button9)
        Me.Panel1.Controls.Add(Me.CheckBox_LayoutLoop)
        Me.Panel1.Controls.Add(Me.CheckBox_SDI)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Button10)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Controls.Add(Me.Button6)
        Me.Panel1.Controls.Add(Me.Button8)
        Me.Panel1.Controls.Add(Me.Button7)
        Me.Panel1.Location = New System.Drawing.Point(0, 273)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(550, 79)
        Me.Panel1.TabIndex = 19
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(129, 29)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(75, 23)
        Me.Button13.TabIndex = 27
        Me.Button13.Text = "Button13"
        Me.Button13.UseVisualStyleBackColor = True
        Me.Button13.Visible = False
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(220, 49)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(130, 23)
        Me.Button12.TabIndex = 26
        Me.Button12.Text = "Speciālās komandas"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'ShowNews
        '
        Me.ShowNews.AutoSize = True
        Me.ShowNews.Location = New System.Drawing.Point(258, 29)
        Me.ShowNews.Name = "ShowNews"
        Me.ShowNews.Size = New System.Drawing.Size(100, 17)
        Me.ShowNews.TabIndex = 25
        Me.ShowNews.Text = "Rādīt jaunumus"
        Me.ShowNews.UseVisualStyleBackColor = True
        '
        'CountMessage
        '
        Me.CountMessage.AutoSize = True
        Me.CountMessage.Location = New System.Drawing.Point(158, 6)
        Me.CountMessage.Name = "CountMessage"
        Me.CountMessage.Size = New System.Drawing.Size(0, 13)
        Me.CountMessage.TabIndex = 24
        '
        'Button11
        '
        Me.Button11.BackgroundImage = Global.EpicGrind.My.Resources.Resources.doc_minus_icon_16
        Me.Button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button11.Location = New System.Drawing.Point(29, 1)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(25, 25)
        Me.Button11.TabIndex = 23
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.BackgroundImage = Global.EpicGrind.My.Resources.Resources.doc_export_icon_16
        Me.Button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button9.Location = New System.Drawing.Point(81, 0)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(25, 25)
        Me.Button9.TabIndex = 22
        Me.Button9.UseVisualStyleBackColor = True
        '
        'CheckBox_LayoutLoop
        '
        Me.CheckBox_LayoutLoop.AutoSize = True
        Me.CheckBox_LayoutLoop.Location = New System.Drawing.Point(423, 30)
        Me.CheckBox_LayoutLoop.Name = "CheckBox_LayoutLoop"
        Me.CheckBox_LayoutLoop.Size = New System.Drawing.Size(122, 17)
        Me.CheckBox_LayoutLoop.TabIndex = 21
        Me.CheckBox_LayoutLoop.Text = "Izpildīt katram layout"
        Me.CheckBox_LayoutLoop.UseVisualStyleBackColor = True
        Me.CheckBox_LayoutLoop.Visible = False
        '
        'CheckBox_SDI
        '
        Me.CheckBox_SDI.AutoSize = True
        Me.CheckBox_SDI.Location = New System.Drawing.Point(364, 29)
        Me.CheckBox_SDI.Name = "CheckBox_SDI"
        Me.CheckBox_SDI.Size = New System.Drawing.Size(53, 17)
        Me.CheckBox_SDI.TabIndex = 20
        Me.CheckBox_SDI.Text = "SDI:1"
        Me.CheckBox_SDI.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.EpicGrind.My.Resources.Resources.Donate_Button2
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox2.Location = New System.Drawing.Point(214, 56)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(54, 20)
        Me.PictureBox2.TabIndex = 19
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 27)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(49, 48)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.EpicGrind.My.Resources.Resources.doc_plus_icon_16
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Location = New System.Drawing.Point(3, 1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 25)
        Me.Button1.TabIndex = 1
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackgroundImage = Global.EpicGrind.My.Resources.Resources.doc_delete_icon_16
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Location = New System.Drawing.Point(55, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 25)
        Me.Button2.TabIndex = 2
        Me.Button2.UseVisualStyleBackColor = True
        '
        'MacroDescription
        '
        Me.MacroDescription.Location = New System.Drawing.Point(220, 189)
        Me.MacroDescription.Name = "MacroDescription"
        Me.MacroDescription.ReadOnly = True
        Me.MacroDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.MacroDescription.Size = New System.Drawing.Size(192, 47)
        Me.MacroDescription.TabIndex = 20
        Me.MacroDescription.Text = "Description Test" & Global.Microsoft.VisualBasic.ChrW(10) & "Test" & Global.Microsoft.VisualBasic.ChrW(10) & "TTT" & Global.Microsoft.VisualBasic.ChrW(10)
        Me.MacroDescription.Visible = False
        '
        'EpicMacroMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 348)
        Me.Controls.Add(Me.MacroDescription)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MacroTable)
        Me.Controls.Add(Me.MacroCommandBox)
        Me.Controls.Add(Me.FileTable)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "EpicMacroMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Epic Macro 1.6.6"
        Me.TopMost = True
        CType(Me.FileTable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MacroTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FileTable As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents MacroCommandBox As RichTextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents MacroTable As DataGridView
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Fails As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Button10 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents CheckBox_LayoutLoop As CheckBox
    Friend WithEvents CheckBox_SDI As CheckBox
    Friend WithEvents Button9 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents CountMessage As Label
    Friend WithEvents MacroDescription As RichTextBox
    Friend WithEvents ShowNews As CheckBox
    Friend WithEvents Button12 As Button
    Friend WithEvents Button13 As Button
End Class
