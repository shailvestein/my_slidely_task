<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnViewSubmission = New System.Windows.Forms.Button()
        Me.btnCreateSubmission = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnDeleteData = New System.Windows.Forms.Button()
        Me.btnUpdateData = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(49, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(760, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Shailesh Kumar Vishwakarma, Slidely Task 2 - Slidely Form App"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnViewSubmission
        '
        Me.btnViewSubmission.BackColor = System.Drawing.Color.Gold
        Me.btnViewSubmission.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewSubmission.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewSubmission.Location = New System.Drawing.Point(211, 178)
        Me.btnViewSubmission.Name = "btnViewSubmission"
        Me.btnViewSubmission.Size = New System.Drawing.Size(442, 32)
        Me.btnViewSubmission.TabIndex = 1
        Me.btnViewSubmission.Text = "VIEW SUBMISSIONS (CTRL + V)"
        Me.btnViewSubmission.UseVisualStyleBackColor = False
        '
        'btnCreateSubmission
        '
        Me.btnCreateSubmission.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnCreateSubmission.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateSubmission.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateSubmission.Location = New System.Drawing.Point(211, 232)
        Me.btnCreateSubmission.Name = "btnCreateSubmission"
        Me.btnCreateSubmission.Size = New System.Drawing.Size(442, 32)
        Me.btnCreateSubmission.TabIndex = 2
        Me.btnCreateSubmission.Text = "CREATE NEW SUBMISSIONS (CTRL + N)"
        Me.btnCreateSubmission.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Red
        Me.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnExit.Location = New System.Drawing.Point(785, 21)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 37)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnDeleteData
        '
        Me.btnDeleteData.BackColor = System.Drawing.Color.DeepPink
        Me.btnDeleteData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteData.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteData.Location = New System.Drawing.Point(211, 283)
        Me.btnDeleteData.Name = "btnDeleteData"
        Me.btnDeleteData.Size = New System.Drawing.Size(442, 32)
        Me.btnDeleteData.TabIndex = 4
        Me.btnDeleteData.Text = "DELETE SUBMISSIONS (CTRL + D)"
        Me.btnDeleteData.UseVisualStyleBackColor = False
        '
        'btnUpdateData
        '
        Me.btnUpdateData.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnUpdateData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateData.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateData.Location = New System.Drawing.Point(211, 336)
        Me.btnUpdateData.Name = "btnUpdateData"
        Me.btnUpdateData.Size = New System.Drawing.Size(442, 32)
        Me.btnUpdateData.TabIndex = 5
        Me.btnUpdateData.Text = "UPDATE SUBMISSIONS (CTRL + U)"
        Me.btnUpdateData.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(882, 503)
        Me.Controls.Add(Me.btnUpdateData)
        Me.Controls.Add(Me.btnDeleteData)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCreateSubmission)
        Me.Controls.Add(Me.btnViewSubmission)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnViewSubmission As Button
    Friend WithEvents btnCreateSubmission As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents btnDeleteData As Button
    Friend WithEvents btnUpdateData As Button
End Class
