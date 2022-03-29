<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cust_EpicSplitW
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TB_DeltaWin = New System.Windows.Forms.TextBox()
        Me.FramePosDelta = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SettingRakstlTags = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TB_DeltaWin)
        Me.GroupBox1.Controls.Add(Me.FramePosDelta)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.SettingRakstlTags)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(222, 172)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Rasējumu opcijas"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 117)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Max rakstl. nobīde:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 145)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Lapas malas rezerve:"
        '
        'TB_DeltaWin
        '
        Me.TB_DeltaWin.Location = New System.Drawing.Point(114, 141)
        Me.TB_DeltaWin.Name = "TB_DeltaWin"
        Me.TB_DeltaWin.Size = New System.Drawing.Size(88, 20)
        Me.TB_DeltaWin.TabIndex = 5
        Me.TB_DeltaWin.Text = "5"
        '
        'FramePosDelta
        '
        Me.FramePosDelta.Location = New System.Drawing.Point(114, 114)
        Me.FramePosDelta.Name = "FramePosDelta"
        Me.FramePosDelta.Size = New System.Drawing.Size(88, 20)
        Me.FramePosDelta.TabIndex = 4
        Me.FramePosDelta.Text = "100"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(156, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Rakstlaukuma TAG ar faila NR:"
        '
        'SettingRakstlTags
        '
        Me.SettingRakstlTags.Location = New System.Drawing.Point(6, 35)
        Me.SettingRakstlTags.Name = "SettingRakstlTags"
        Me.SettingRakstlTags.Size = New System.Drawing.Size(196, 73)
        Me.SettingRakstlTags.TabIndex = 1
        Me.SettingRakstlTags.Text = ""
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(150, 178)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CustomComOpt_Split
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(228, 204)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CustomComOpt_Split"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EpicSplitToFiles Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TB_DeltaWin As TextBox
    Friend WithEvents FramePosDelta As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents SettingRakstlTags As RichTextBox
    Friend WithEvents Button1 As Button
End Class
