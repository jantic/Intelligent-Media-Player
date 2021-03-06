﻿Imports System.Drawing.Image
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class LastFMArtistInfo
    Implements IArtistInfo
    Private myName As String
    Private Const myTemplateArtistInfoURL As String = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=[Artist]&api_key=e295ea662af320c44101419cb30cfffe"
    Private Const myTemplateTopAlbumsURL As String = "http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=[Artist]&api_key=e295ea662af320c44101419cb30cfffe"
    Private myArtistInfoResultXML As XmlDocument = Nothing 'cached because otherwise last.fm will kick this program out for too many calls.
    Private myTopAlbumsResultXML As XmlDocument = Nothing

    Public Sub New(ByRef artistName As String)
        myName = artistName
        RetrieveAndStoreArtistInfoFromWebservice()
    End Sub


    Public ReadOnly Property Biography As String Implements IArtistInfo.Biography
        Get
            If (myArtistInfoResultXML Is Nothing) Then
                Return ""
            End If

            Dim biographyXpathQuery As String = "//lfm/artist/bio/content"
            Return WebServiceClient.GetClient.XMLGetValueAt(myArtistInfoResultXML, biographyXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IArtistInfo.Name
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property PictureLocation As String Implements IArtistInfo.PictureLocation
        Get
            If (myArtistInfoResultXML Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//lfm/artist/image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myArtistInfoResultXML, imageXpathQuery, "size", "extralarge")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
        End Get
    End Property

    Public ReadOnly Property SmallPictureLocation As String Implements IArtistInfo.SmallPictureLocation
        Get
            If (myArtistInfoResultXML Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//lfm/artist/image"
            Dim imageUrl As String = WebServiceClient.GetClient.XMLGetValueAt(myArtistInfoResultXML, imageXpathQuery, "size", "medium")
            Return WebServiceClient.GetClient.RetrieveImageLocationToUse(imageUrl)
        End Get
    End Property

    Public ReadOnly Property WebLink As String Implements IArtistInfo.WebLink
        Get
            If (myArtistInfoResultXML Is Nothing) Then
                Return ""
            End If

            Dim weblinkXpathQuery As String = "//lfm/artist/url"
            Return WebServiceClient.GetClient.XMLGetValueAt(myArtistInfoResultXML, weblinkXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property Summary As String Implements IArtistInfo.Summary
        Get
            If (myArtistInfoResultXML Is Nothing) Then
                Return ""
            End If

            Dim summaryXpathQuery As String = "//lfm/artist/bio/summary"
            Return WebServiceClient.GetClient.XMLGetValueAt(myArtistInfoResultXML, summaryXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property SimilarArtists As IArtistNameFace() Implements IArtistInfo.SimilarArtists
        Get
            If (myArtistInfoResultXML Is Nothing) Then
                Return Nothing
            End If

            Dim artists As ArrayList = New ArrayList()
            Dim similarArtistXPathQuery As String = "//lfm/artist/similar/artist"
            Dim similarArtistXMLs As XmlNodeList = myArtistInfoResultXML.SelectNodes(similarArtistXPathQuery)

            For Each artistXML As XmlElement In similarArtistXMLs
                artists.Add(New LastFMArtistNameFace(artistXML))
            Next

            Return artists.ToArray(GetType(IArtistNameFace))
        End Get
    End Property

    Public ReadOnly Property Tags As String() Implements IArtistInfo.Tags
        Get
            If (myArtistInfoResultXML Is Nothing) Then
                Return Nothing
            End If

            Dim tagsList As ArrayList = New ArrayList()
            Dim tagXpathQuery As String = "//lfm/artist/tags/tag/name"
            Dim index As UInteger = 0
            Dim tagName As String = WebServiceClient.GetClient.XMLGetValueAt(myArtistInfoResultXML, tagXpathQuery, index)

            While (Not tagName Is Nothing)
                tagsList.Add(tagName)
                index += 1
                tagName = WebServiceClient.GetClient.XMLGetValueAt(myArtistInfoResultXML, tagXpathQuery, index)
            End While

            Return tagsList.ToArray(GetType(String))
        End Get
    End Property

    Public ReadOnly Property TopAlbums As IAlbumInfo() Implements IArtistInfo.TopAlbums
        Get

            RetrieveAndStoreTopAlbumsInfoFromWebService()

            If (myTopAlbumsResultXML Is Nothing) Then
                Return Nothing
            End If

            Dim albumsList As ArrayList = New ArrayList()
            Dim index As UInteger = 0


            Dim albumNodes As XmlNodeList = myTopAlbumsResultXML.GetElementsByTagName("album")

            'limiting to 9 for performance reasons.
            'Dim count As Integer = 1

            For Each albumElement As XmlElement In albumNodes
                'If (count > 9) Then
                'Exit For
                'End If
                albumsList.Add(New LastFMAlbumInfo(albumElement, Me))
                'count += 1
            Next

            Return albumsList.ToArray(GetType(IAlbumInfo))
        End Get
    End Property

    Private Sub RetrieveAndStoreArtistInfoFromWebservice()
        If (Not myName Is Nothing And Not myName = "") Then
            Dim artistInfoURL As String = myTemplateArtistInfoURL.Replace("[Artist]", myName)
            myArtistInfoResultXML = WebServiceClient.GetClient.RetrieveResult(artistInfoURL)
        End If

    End Sub

    Private Sub RetrieveAndStoreTopAlbumsInfoFromWebService()
        If (myTopAlbumsResultXML Is Nothing And Not myName Is Nothing And Not myName = "") Then
            Dim topAlbumInfoURL As String = myTemplateTopAlbumsURL.Replace("[Artist]", myName)
            myTopAlbumsResultXML = WebServiceClient.GetClient.RetrieveResult(topAlbumInfoURL)
        End If
    End Sub


End Class
