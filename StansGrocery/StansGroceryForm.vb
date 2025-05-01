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
                    currentRecord = Replace(currentRecord, "*", "")
                    If currentRecord = vbLf Then
                        currentIndex(currentRow, 0) = ("")
                    ElseIf currentRecord <> "" Then
                        currentIndex(currentRow, 0) = ($" {currentRecord}")
                    End If
                    Input(fileNumber, currentRecord)
                    currentRecord = Replace(currentRecord, "##LOC", "Aisle ")
                    If currentRecord <> " then" Then
                        currentIndex(currentRow, 1) = currentRecord
                    End If
                    Input(fileNumber, currentRecord)
                    currentRecord = Replace(currentRecord, "%%CAT", "")
                    If currentRecord <> "" Then
                        currentIndex(currentRow, 2) = currentRecord
                    End If
                    currentRow += 1
                Loop
                FileClose(fileNumber)
            Catch ex As Exception
            End Try
        Loop While InvalidFileName
        ItemArray(currentIndex)
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
        Dim items(,) As String = ItemArray()
        If items IsNot Nothing Then
            For i = 0 To items.GetUpperBound(0) 'UBound(_customers)
                If items(i, 0) <> "" Then
                    DisplayListBox.Items.Add($"{items(i, 0)}")
                    DisplayListBox.SelectedIndex() = 0
                End If
            Next
        End If
    End Sub
    Function ItemArray(Optional itemData(,) As String = Nothing) As String(,)
        Static Items(,) As String

        If itemData IsNot Nothing Then
            Items = itemData
        End If
        Return Items
    End Function
    '**************************** Event Handlers*******************************
    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ReadFromFile()
        DisplayData()
        FilterByCategoryRadioButton.Checked = True
        FilterByAisleRadioButton.Checked = False
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub FilterByCategoryRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles FilterByCategoryRadioButton.CheckedChanged, FilterByAisleRadioButton.CheckedChanged
        Dim items(,) As String = ItemArray()
        If FilterByAisleRadioButton.Checked Then
            FilterComboBox.Items.Clear()
            FilterComboBox.Items.Add("Aisle ")
            FilterComboBox.Items.Add("Aisle 1")
            FilterComboBox.Items.Add("Aisle 2")
            FilterComboBox.Items.Add("Aisle 3")
            FilterComboBox.Items.Add("Aisle 4")
            FilterComboBox.Items.Add("Aisle 5")
            FilterComboBox.Items.Add("Aisle 6")
            FilterComboBox.Items.Add("Aisle 7")
            FilterComboBox.Items.Add("Aisle 8")
            FilterComboBox.Items.Add("Aisle 9")
            FilterComboBox.Items.Add("Aisle 10")
            FilterComboBox.Items.Add("Aisle 11")
            FilterComboBox.Items.Add("Aisle 12")
            FilterComboBox.Items.Add("Aisle 13")
            FilterComboBox.Items.Add("Aisle 14")
            FilterComboBox.Items.Add("Aisle 15")
            FilterComboBox.Items.Add("Aisle 16")
            FilterComboBox.Items.Add("Aisle 17")
        ElseIf FilterByCategoryRadioButton.Checked Then
            FilterComboBox.Items.Clear()
            If items IsNot Nothing Then
                FilterComboBox.Items.Add("Fresh Fruit")
                FilterComboBox.Items.Add("Frozen")
                FilterComboBox.Items.Add("Condiments / Sauces")
                FilterComboBox.Items.Add("Various groceries")
                FilterComboBox.Items.Add("Canned foods")
                FilterComboBox.Items.Add("Spices & herbs")
                FilterComboBox.Items.Add("Dairy")
                FilterComboBox.Items.Add("Cheese")
                FilterComboBox.Items.Add("Meat")
                FilterComboBox.Items.Add("Seafood")
                FilterComboBox.Items.Add("Beverages")
                FilterComboBox.Items.Add("Baked goods")
                FilterComboBox.Items.Add("Baking")
                FilterComboBox.Items.Add("Snacks")
                FilterComboBox.Items.Add("Themed meals")
                FilterComboBox.Items.Add("Baby stuff")
                FilterComboBox.Items.Add("Pets")
                FilterComboBox.Items.Add("Personal care")
                FilterComboBox.Items.Add("Pharmacy")
                FilterComboBox.Items.Add("Kitchen")
                FilterComboBox.Items.Add("Cleaning products")
                FilterComboBox.Items.Add("Office supplies")
                FilterComboBox.Items.Add("Other stuff")
            End If
        End If
    End Sub
    Private Sub FilterComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FilterComboBox.SelectedIndexChanged
        DisplayListBox.Items.Clear()
        Dim items(,) As String = ItemArray()
        If FilterByAisleRadioButton.Checked Then
            DisplayListBox.Items.Clear()
            If items IsNot Nothing Then
                For i = 0 To items.GetUpperBound(0) 'UBound(_customers)
                    If items(i, 1) = $"{CStr(FilterComboBox.SelectedItem)}" Then
                        DisplayListBox.Items.Add($"{items(i, 0)}")
                    End If
                Next
            End If
        ElseIf FilterByCategoryRadioButton.Checked Then
            DisplayListBox.Items.Clear()
            If items IsNot Nothing Then
                For i = 0 To items.GetUpperBound(0) 'UBound(_customers)
                    If items(i, 2) = $"{CStr(FilterComboBox.SelectedItem)}" Then
                        DisplayListBox.Items.Add($"{items(i, 0)}")
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        FilterByCategoryRadioButton.Checked = True
        FilterByAisleRadioButton.Checked = False
        SearchTextBox.Text = ""
        DisplayListBox.Items.Clear()
        DisplayData()
    End Sub
    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Dim items(,) As String = ItemArray()
        If items IsNot Nothing Then
            DisplayListBox.Items.Clear()
            For i = 0 To items.GetUpperBound(0) 'UBound(_customers)
                If InStr(items(i, 0), SearchTextBox.Text) Then
                    DisplayListBox.Items.Add($"{items(i, 0)}")
                End If
            Next
        End If
    End Sub
End Class
