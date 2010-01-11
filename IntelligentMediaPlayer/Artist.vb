Imports System.Xml

Public Class Artist
    Private myName As String
    Private myMatchFactor As Double

    Public Sub New(ByVal name As String, ByVal matchFactor As Double)
        myName = name
        myMatchFactor = matchFactor
    End Sub


    Public ReadOnly Property Name() As String
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property MatchFactor() As String
        Get
            Return myMatchFactor
        End Get
    End Property

End Class
