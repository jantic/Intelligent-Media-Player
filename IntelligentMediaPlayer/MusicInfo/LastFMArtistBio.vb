Imports System.Drawing.Image
Imports System.Xml
Imports System.Xml.XPath

Public Class LastFMArtistBio
    Implements IArtistBio
    Private myName As String
    Private Const myTemplateURL As String = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=[Artist]&api_key=e295ea662af320c44101419cb30cfffe"
    Private myCallResultXML As XmlDocument 'cached because otherwise last.fm will kick this program out for too many calls.

    Public Sub New(ByRef artistName As String)
        myName = artistName
        RetrieveAndStoreResultsFromWebservice()
    End Sub


    Public ReadOnly Property Biography As String Implements IArtistBio.Biography
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim biographyXpathQuery As String = "//lfm/artist/bio/content"
            Return XMLGetValueAt(myCallResultXML, biographyXpathQuery, 0)
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IArtistBio.Name
        Get
            Return myName
        End Get
    End Property

    Public ReadOnly Property PictureLocation As String Implements IArtistBio.PictureLocation
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim imageXpathQuery As String = "//lfm/artist/image"
            Return XMLGetValueAt(myCallResultXML, imageXpathQuery, "size", "extralarge")
        End Get
    End Property

    Public ReadOnly Property Summary As String Implements IArtistBio.Summary
        Get
            If (myCallResultXML Is Nothing) Then
                Return ""
            End If

            Dim summaryXpathQuery As String = "//lfm/artist/bio/summary"
            Return XMLGetValueAt(myCallResultXML, summaryXpathQuery, 0)
        End Get
    End Property

    Private Sub RetrieveAndStoreResultsFromWebservice()
        Dim url As String = myTemplateURL.Replace("[Artist]", myName)
        myCallResultXML = New XmlDocument()
        Try
            myCallResultXML.Load(url)
        Catch ex As Exception
            myCallResultXML = Nothing
        End Try
    End Sub

    Private Shared Function XMLGetValueAt(ByRef document As XmlDocument, ByVal pathQuery As String, ByVal index As UInteger) As String
        Dim theElement As Xml.XmlElement = Nothing

        Try
            Dim nodes As XmlNodeList = document.SelectNodes(pathQuery)

            If (nodes.Count >= index + 1) Then
                theElement = nodes.Item(index)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        If (Not theElement Is Nothing) Then
            Dim value As String = theElement.InnerText
            Return value
        End If

        Return Nothing
    End Function

    Private Shared Function XMLGetValueAt(ByRef document As XmlDocument, ByVal pathQuery As String, ByVal attributeName As String, ByVal attributeValue As String) As String
        Dim theElement As Xml.XmlElement = Nothing

        Try
            Dim nodes As XmlNodeList = document.SelectNodes(pathQuery)

            For Each element As XmlElement In nodes
                If (element.GetAttribute(attributeName) = attributeValue) Then
                    theElement = element
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        If (Not theElement Is Nothing) Then
            Dim value As String = theElement.InnerText
            Return value
        End If

        Return Nothing
    End Function
End Class
