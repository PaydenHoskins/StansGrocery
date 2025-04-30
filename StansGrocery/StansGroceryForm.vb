Imports System.IO

Public Class StansGroceryForm
    Sub ReadFromFile()
        Dim filePath As String = "Grocery.txt"
        Dim fileNumber As Integer = FreeFile()
        Dim currentRecord As String = ""
        Dim InvalidFileName As Boolean = True
        Dim list As String(,)
        Dim currentIndex(ListOfGrocery(filePath), 2) As String
        Dim currentRow As Integer = 0
        Do
            Try
                FileOpen(fileNumber, filePath, OpenMode.Input)
                InvalidFileName = False
                Do Until EOF(fileNumber)
                    Input(fileNumber, currentRecord)
                    currentRecord = Replace(currentRecord, "$$ITM", "")
                    currentRecord = Replace(currentRecord, """", "")
                    currentIndex(currentRow, 0) = currentRecord
                    Input(fileNumber, currentRecord)
                    currentRecord = Replace(currentRecord, "##LOC", "Aisle ")
                    currentIndex(currentRow, 1) = currentRecord
                    Input(fileNumber, currentRecord)
                    currentRecord = Replace(currentRecord, "%%CAT", "")
                    currentIndex(currentRow, 2) = currentRecord
                    currentRow += 1
                Loop
                FileClose(fileNumber)
            Catch ex As Exception
            End Try
        Loop While InvalidFileName
        CustomersArray(currentIndex)
    End Sub
    Function ListOfGrocery(filepath As String) As Integer
        Dim count As Integer = 0
        Dim fileNumber As Integer = FreeFile()
        Try
            FileOpen(fileNumber, filepath, OpenMode.Input)
            Do Until EOF(fileNumber)
                LineInput(fileNumber)
                count += 1
            Loop
            FileClose(fileNumber)
        Catch ex As Exception
            'pass
        End Try
        Return count
    End Function
    Sub DisplayData()
        Dim items(,) As String = CustomersArray()
        If items IsNot Nothing Then
            For i = 0 To items.GetUpperBound(0) 'UBound(_customers)
                DisplayListBox.Items.Add($"{items(i, 0)} ,{items(i, 1)} ,{items(i, 2)}")
                DisplayListBox.SelectedIndex() = 0
            Next
        End If
    End Sub
    Function CustomersArray(Optional customerData(,) As String = Nothing) As String(,)
        Static _customers(,) As String

        If customerData IsNot Nothing Then
            _customers = customerData
        End If
        Return _customers
    End Function
    '**************************** Event Handlers*******************************
    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ReadFromFile()
        DisplayData()
    End Sub
End Class
