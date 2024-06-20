Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks

Public Class FormViewSubmission
    Private submissions As List(Of Dictionary(Of String, String))
    Private currentIndex As Integer = 0

    Private Async Sub FormViewSubmission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        submissions = Await GetDataAsync()
        If submissions.Count > 0 Then
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub FormViewSubmission_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()

        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()

        ElseIf e.KeyCode = Keys.Escape Then
            btnBack.PerformClick()
        End If
    End Sub

    Private Sub DisplaySubmission(index As Integer)
        If index >= 0 AndAlso index < submissions.Count Then
            Dim submission = submissions(index)
            txtName.Text = submission("name")
            txtEmail.Text = submission("email")
            txtPhone.Text = submission("phone")
            txtGitHub.Text = submission("githubLink")
            lblTimer.Text = submission("timeSpent")
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Async Function GetDataAsync() As Task(Of List(Of Dictionary(Of String, String)))
        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/read")
            response.EnsureSuccessStatusCode()

            Dim responseBody As String = Await response.Content.ReadAsStringAsync()
            Return JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, String)))(responseBody)
        End Using
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()

    End Sub
End Class
