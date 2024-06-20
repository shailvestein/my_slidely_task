Imports System.Windows.Forms

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormHomePage_Load: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso e.KeyCode = Keys.N Then
                btnCreateSubmission.PerformClick()
            ElseIf e.Control AndAlso e.KeyCode = Keys.V Then
                btnViewSubmission.PerformClick()
            ElseIf e.KeyCode = Keys.Escape Then
                btnExit.PerformClick()
            ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
                btnDeleteData.PerformClick()
            ElseIf e.Control AndAlso e.KeyCode = Keys.U Then
                btnUpdateData.PerformClick()
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormHomePage_KeyDown: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        Try
            Dim createForm As New FormCreateSubmission()
            createForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnCreateSubmission_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnViewSubmission_Click(sender As Object, e As EventArgs) Handles btnViewSubmission.Click
        Try
            Dim viewForm As New FormViewSubmission()
            viewForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnViewSubmission_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnExit_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteData_Click(sender As Object, e As EventArgs) Handles btnDeleteData.Click
        Try
            Dim deleteForm As New FormDelete()
            deleteForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnDeleteData_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdateData_Click(sender As Object, e As EventArgs) Handles btnUpdateData.Click
        Try
            Dim updateForm As New FormUpdate()
            updateForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnUpdateData_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
