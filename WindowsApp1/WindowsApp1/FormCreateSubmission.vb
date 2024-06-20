Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Timers
Imports System.Text.RegularExpressions

Public Class FormCreateSubmission
    Private timer As New Timers.Timer(1000)
    Private elapsedTime As TimeSpan = TimeSpan.Zero

    Private Sub FormCreateSubmission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.KeyPreview = True
            AddHandler timer.Elapsed, AddressOf OnTimedEvent
        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormCreateSubmission_Load: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormCreateSubmission_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Try
            If e.Control AndAlso e.KeyCode = Keys.S Then
                btnSubmit.PerformClick()
            End If

            If e.Control AndAlso e.KeyCode = Keys.T Then
                ToggleStopwatch()
            End If

            If e.KeyCode = Keys.Escape Then
                btnBack.PerformClick()
            End If

        Catch ex As Exception
            MessageBox.Show($"An error occurred in FormCreateSubmission_KeyDown: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ToggleStopwatch()
        Try
            If timer.Enabled Then
                timer.Stop()
                btnToggleTimer.Text = "Resume StopWatch"
            Else
                timer.Start()
                btnToggleTimer.Text = "Pause StopWatch"
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred in ToggleStopwatch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnTimedEvent(source As Object, e As Timers.ElapsedEventArgs)
        Try
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1))
            lblTimer.Invoke(Sub() lblTimer.Text = elapsedTime.ToString("hh\:mm\:ss"))
        Catch ex As Exception
            MessageBox.Show($"An error occurred in OnTimedEvent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Check if any of the fields are empty
        Try
            If String.IsNullOrWhiteSpace(txtName.Text) OrElse
               String.IsNullOrWhiteSpace(txtEmail.Text) OrElse
               String.IsNullOrWhiteSpace(txtPhone.Text) OrElse
               String.IsNullOrWhiteSpace(txtGitHub.Text) Then
                MessageBox.Show("All fields are required!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If



            If Not ValidateName(txtName.Text) Then
                MessageBox.Show("Name must contain only letters and can include space-separated parts like first name, middle name, last name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If Not ValidateEmail(txtEmail.Text) Then
                MessageBox.Show("Invalid email address format!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If Not ValidatePhoneNumber(txtPhone.Text) Then
                MessageBox.Show("Phone number must be exactly 10 digits!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If Not ValidateGitHubLink(txtGitHub.Text) Then
                MessageBox.Show("Invalid GitHub link!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            timer.Stop()
            Await SubmitDataAsync(txtName.Text, txtEmail.Text, txtPhone.Text, txtGitHub.Text, lblTimer.Text)
            MsgBox("Record saved successfully")
            txtName.Focus()
            txtName.Clear()
            txtEmail.Clear()
            txtPhone.Clear()
            txtGitHub.Clear()
            lblTimer.Text = "00:00:00"
            btnToggleTimer.Text = "TOGGLE STOPWATCH (CTRL + T)"
        Catch ex As Exception
            MessageBox.Show($"An error occurred in btnSubmit_Click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Async Function SubmitDataAsync(name As String, email As String, phone As String, githubLink As String, timeSpent As String) As Task
        Try
            Using client As New HttpClient()
                Dim data As New Dictionary(Of String, String) From {
                    {"name", name},
                    {"email", email},
                    {"phone", phone},
                    {"githubLink", githubLink},
                    {"timeSpent", timeSpent}
                }
                Dim json As String = JsonConvert.SerializeObject(data)
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")

                Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)
                response.EnsureSuccessStatusCode()
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred in SubmitDataAsync: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub btnToggleTimer_Click(sender As Object, e As EventArgs) Handles btnToggleTimer.Click
        ToggleStopwatch()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        timer.Stop()
        Me.Close()
    End Sub

    Private Function ValidateName(name As String) As Boolean
        Try
            ' Check if name contains only letters and spaces (allowing for first name, middle name, last name)
            Dim pattern As String = "^[A-Za-z]+(?:\s[A-Za-z]+)*$"
            Dim regex As New Regex(pattern)
            Return regex.IsMatch(name)
        Catch ex As Exception
            MessageBox.Show($"An error occurred in ValidateName: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

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

    Private Function ValidatePhoneNumber(phone As String) As Boolean
        ' Check if phone number is exactly 10 digits
        Try
            Dim pattern As String = "^\d{10}$"
            Dim regex As New Regex(pattern)
            Return regex.IsMatch(phone)
        Catch ex As Exception
            MessageBox.Show($"An error occurred in ValidatePhoneNumber: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function ValidateGitHubLink(githubLink As String) As Boolean
        ' Regex pattern to validate GitHub link
        Try
            Dim pattern As String = "^https:\/\/github\.com\/[A-Za-z0-9_-]+(\/[A-Za-z0-9_-]+)?$"
            Dim regex As New Regex(pattern)
            Return regex.IsMatch(githubLink)
        Catch ex As Exception
            MessageBox.Show($"An error occurred in ValidateGitHubLink: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

End Class
