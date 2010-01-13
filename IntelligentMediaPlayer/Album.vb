Public Class Album
    Private myName As String
    Private myTagCount As UInteger

    Public Sub New(ByVal name As String, ByVal tagCount As UInteger)
        myName = name
        myTagCount = tagCount
    End Sub

    Public ReadOnly Property Name As String
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property TagCount As UInteger
        Get
            Return myTagCount
        End Get
    End Property
End Class
