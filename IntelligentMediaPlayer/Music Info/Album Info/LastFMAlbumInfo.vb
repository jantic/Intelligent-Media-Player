Imports System.Xml

Public Class LastFMAlbumInfo
    Implements IAlbumInfo

    Private myXMLElement As XmlElement
    Private myArtist As IArtistInfo
    Private Const myTemplateAlbumInfoURL As String = "http://ws.audioscrobbler.com/2.0/?method=album.getinfo&artist=[Artist]&album=[Album]&api_key=e295ea662af320c44101419cb30cfffe"

    Public Sub New(ByVal theXMLElement As XmlElement, ByRef theArtist As IArtistInfo)
        myXMLElement = theXMLElement.Clone
        myArtist = theArtist
    End Sub

    Public Sub New(ByVal artistName As String, ByVal albumName As String)
        myArtist = New LastFMArtistInfo(artistName)
        RetrieveAndStoreAlbumInfoFromWebservice(albumName)
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

    Public ReadOnly Property LargePictureLocation As String Implements IAlbumInfo.LargePictureLocation
        Get
            If (myXMLElement Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myXMLElement, imageXpathQuery, "size", "extralarge")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
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

    Private Sub RetrieveAndStoreAlbumInfoFromWebservice(ByVal albumName As String)
        If (Not albumName Is Nothing And Not albumName = "") Then
            Dim albumInfoURL As String = myTemplateAlbumInfoURL.Replace("[Album]", albumName).Replace("[Artist]", myArtist.Name)
            Dim myAlbumInfoResultXML As XmlDocument = WebServiceClient.GetClient.RetrieveResult(albumInfoURL)

            If (Not myAlbumInfoResultXML Is Nothing) Then
                myXMLElement = myAlbumInfoResultXML.GetElementsByTagName("album").Item(0)
            End If
        End If

    End Sub
End Class
