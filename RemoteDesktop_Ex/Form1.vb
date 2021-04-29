Imports MSTSCLib

Public Class Form1
    Private Function a() As Boolean
        ' Get our process name.
        Dim my_proc As Process = Process.GetCurrentProcess
            Dim my_name As String = my_proc.ProcessName

            ' Get information about processes with this name.
            Dim procs() As Process =
        Process.GetProcessesByName(my_name)

            ' If there is only one, it's us.
            If procs.Length = 1 Then Return False

            ' If there is more than one process,
            ' see if one has a StartTime before ours.
            Dim i As Integer
            For i = 0 To procs.Length - 1
                If procs(i).StartTime < my_proc.StartTime Then _
            Return True
            Next i

            ' If we get here, we were first.
            Return False

    End Function

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click

        Try

            rdpView.Server = txtServer.Text
            rdpView.UserName = txtUserName.Text

            Dim isSecured As IMsTscNonScriptable = DirectCast(rdpView.GetOcx(), IMsTscNonScriptable)

            isSecured.ClearTextPassword = txtPassword.Text

            rdpView.Connect()

        Catch ex As Exception

            MessageBox.Show("Không kết nối được", "Không kết nối đến được: " + txtServer.Text + " Nguyên nhân :  " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click

        Try

            If rdpView.Connected.ToString() = "1" Then

                rdpView.Disconnect()
                MsgBox("Đã hủy", vbOKOnly + vbExclamation, "Thông báo!!!")

            End If

        Catch ex As Exception

            MessageBox.Show("Không thể hủy", "Không thể hủy từ địa chỉ: " + txtServer.Text + " Nguyên nhân:  " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.UseSystemPasswordChar = True
        If a() Then
            MessageBox.Show(
                "Ứng dụng đã được bật",
                "Ứng dụng đang chạy",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation)
            Me.Close()
        End If
    End Sub
End Class
