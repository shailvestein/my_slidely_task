Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json


Public Class FormDelete

    Private Sub FormDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormDelete_Load: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormDelete_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                btnBack.PerformClick()
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormDelete_KeyDown: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim email As String = txtEmail.Text
            If String.IsNullOrWhiteSpace(email) Then
                MessageBox.Show("Please enter an email address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If Not ValidateEmail(txtEmail.Text) Then
                MessageBox.Show("Invalid email address format!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If


            SearchEntryAsync(email)
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnSearch_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub SearchEntryAsync(email As String)
        Try
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.GetAsync($"http://localhost:3000/read/{email}")
                If response.IsSuccessStatusCode Then
                    Dim data As String = Await response.Content.ReadAsStringAsync()
                    MessageBox.Show("Data fetched successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' For debugging purposes, print the fetched data
                    ' MessageBox.Show($"Fetched data: {data}", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Dim entry As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(data)
                    If entry IsNot Nothing AndAlso entry.ContainsKey("email") Then
                        lblResult.Text = $"Name: {entry("name")}" & vbCrLf &
                                        $"Email: {entry("email")}" & vbCrLf &
                                        $"Phone: {entry("phone")}" & vbCrLf &
                                        $"GitHub: {entry("githubLink")}" & vbCrLf &
                                        $"Time Spent: {entry("timeSpent")}"


                    End If
                Else
                    MessageBox.Show("No data found for the provided email address.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lblResult.Text = ""
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred in SearchEntryAsync: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim email As String = txtEmail.Text
            If String.IsNullOrWhiteSpace(email) Then
                MessageBox.Show("Please enter an email address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.DeleteAsync($"http://localhost:3000/delete/{email}")
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lblResult.Text = ""
                    txtEmail.Clear()
                Else
                    MessageBox.Show($"{email} Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnDelete_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show($"An error occured in btnBack_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtEmail.Clear()
        txtEmail.Focus()
    End Sub

    Private Function ValidateEmail(email As String) As Boolean
        ' Regex pattern to validate email
        Try
            Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
            Dim regex As New Regex(pattern)
            Return regex.IsMatch(email)
        Catch ex As Exception
            MessageBox.Show($"An error occurred in ValidateEmail: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

End Class
