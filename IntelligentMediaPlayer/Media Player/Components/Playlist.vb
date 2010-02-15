Imports WMPLib
Imports AxWMPLib

Public Class Playlist
    Private myMediaItems As New ArrayList
    Private myName As String

    Public Sub New()

    End Sub

    Public Sub New(ByRef mediaItems As ArrayList)
        myMediaItems = mediaItems
    End Sub

    Public Sub New(ByRef mediaItems As Media())
        myMediaItems = New ArrayList(mediaItems)
    End Sub

    Public Sub appendItem(ByVal pIWMPMedia As Media)
        myMediaItems.Add(pIWMPMedia)
    End Sub

    Public Sub clear()
        myMediaItems.Clear()
    End Sub

    Public ReadOnly Property count As Integer
        Get
            Return myMediaItems.Count
        End Get
    End Property

    Public Sub insertItem(ByVal lIndex As Integer, ByVal pIWMPMedia As Media)
        myMediaItems.Insert(lIndex, pIWMPMedia)
    End Sub


    Public ReadOnly Property Item(ByVal lIndex As Integer) As Media
        Get
            Return DirectCast(myMediaItems.Item(lIndex), Media)
        End Get
    End Property


    Public Property name As String
        Get
            Return myName
        End Get
        Set(ByVal value As String)
            myName = name
        End Set
    End Property

    Public Sub removeItem(ByVal pIWMPMedia As Media)
        myMediaItems.Remove(pIWMPMedia)
    End Sub

End Class
