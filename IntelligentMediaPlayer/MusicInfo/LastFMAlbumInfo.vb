Imports System.Xml

Public Class LastFMAlbumInfo
    Implements IAlbumInfo

    Private myXMLElement As XmlElement
    Private myArtist As IArtistInfo

    Public Sub New(ByVal theXMLElement As XmlElement, ByRef theArtist As IArtistInfo)
        myXMLElement = theXMLElement.Clone
        myArtist = theArtist
    End Sub

    Public ReadOnly Property Artist As IArtistInfo Implements IAlbumInfo.Artist
        Get
            Return myArtist
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IAlbumInfo.Name
        Get
            If (myXMLElement Is Nothing) Then
                Return ""
            End If

            Dim nameXpathQuery As String = "//name"
            Return WebServiceClient.GetClient.XMLGetValueAt(myXMLElement, nameXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property PictureLocation As String Implements IAlbumInfo.PictureLocation
        Get
            If (myXMLElement Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myXMLElement, imageXpathQuery, "size", "large")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
        End Get
    End Property

    Public ReadOnly Property SmallPictureLocation As String Implements IAlbumInfo.SmallPictureLocation
        Get
            If (myXMLElement Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myXMLElement, imageXpathQuery, "size", "medium")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
        End Get
    End Property

    Public ReadOnly Property WebLink As String Implements IAlbumInfo.WebLink
        Get
            If (myXMLElement Is Nothing) Then
                Return ""
            End If

            Dim weblinkXpathQuery As String = "//url"
            Return WebServiceClient.GetClient.XMLGetValueAt(myXMLElement, weblinkXpathQuery, 0)
        End Get
    End Property
End Class
