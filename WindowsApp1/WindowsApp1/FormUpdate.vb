Imports System.Net.Http
Imports Newtonsoft.Json

Public Class FormUpdate
    Private Sub FormUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormUpdate_Load: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormUpdate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                btnBack.PerformClick()
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormUpdate_KeyDown: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim email As String = txtEmail.Text.Trim()
            If String.IsNullOrWhiteSpace(email) Then
                MessageBox.Show("Please enter an email address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Await SearchEntryAsync(email)
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnSearch_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Function SearchEntryAsync(email As String) As Task
        Try
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.GetAsync($"http://localhost:3000/read/{email}")
                If response.IsSuccessStatusCode Then
                    Dim data As String = Await response.Content.ReadAsStringAsync()
                    Dim entry As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(data)
                    If entry IsNot Nothing AndAlso entry.ContainsKey("email") Then
                        txtName.Text = entry("name")
                        txtPhone.Text = entry("phone")
                        txtGitHub.Text = entry("githubLink")
                        txtTimer.Text = entry("timeSpent")

                        txtName.ReadOnly = False
                        txtPhone.ReadOnly = False
                        txtGitHub.ReadOnly = False
                    Else
                        MessageBox.Show("No data found for the provided email address.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtName.Text = ""
                        txtPhone.Text = ""
                        txtGitHub.Text = ""
                        txtTimer.Text = ""
                    End If
                Else
                    MessageBox.Show("An error occurred while fetching data. Status Code: " & response.StatusCode, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ClearFields()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred in SearchEntryAsync: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim email As String = txtEmail.Text.Trim()
            Dim name As String = txtName.Text.Trim()
            Dim phone As String = txtPhone.Text.Trim()
            Dim githubLink As String = txtGitHub.Text.Trim()

            ' Validate input fields
            If String.IsNullOrWhiteSpace(email) OrElse
               String.IsNullOrWhiteSpace(name) OrElse
               String.IsNullOrWhiteSpace(phone) OrElse
               String.IsNullOrWhiteSpace(githubLink) Then
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Update data in database
            Await UpdateEntryAsync(email, name, phone, githubLink)
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnUpdate_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Function UpdateEntryAsync(email As String, name As String, phone As String, githubLink As String) As Task
        Try
            Using client As New HttpClient()
                Dim data As New Dictionary(Of String, String) From {
                    {"email", email},
                    {"name", name},
                    {"phone", phone},
                    {"githubLink", githubLink}
                }
                Dim json = JsonConvert.SerializeObject(data)
                Dim content = New StringContent(json, System.Text.Encoding.UTF8, "application/json")

                Dim response As HttpResponseMessage = Await client.PutAsync($"http://localhost:3000/update/{email}", content)
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ClearFields()
                    txtEmail.Focus()

                    txtName.ReadOnly = True
                    txtPhone.ReadOnly = True
                    txtGitHub.ReadOnly = True
                Else
                    Dim errorMessage = Await response.Content.ReadAsStringAsync()
                    MessageBox.Show($"An error occurred while updating data. Status Code: {response.StatusCode}, Error: {errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred in UpdateEntryAsync: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnBack_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearFields()
        txtName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtGitHub.Clear()
        txtTimer.Clear()
    End Sub

    Private Sub txtClear_Click(sender As Object, e As EventArgs) Handles txtClear.Click
        ClearFields()
    End Sub
End Class
