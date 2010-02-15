Imports System.Threading.Tasks
Imports WMPLib
Imports AxWMPLib

Public Class MediaCollection
    'First lookup attribute name, which returns a media item lookup for that attribute by the attribute value.
    Private myAttributeLookup As New Dictionary(Of String, Dictionary(Of String, ArrayList))
    Private allTracks As ArrayList = Nothing
    Private Shared myMediaCollectionSingleton As MediaCollection = Nothing


    Public Shared Function GetMediaCollection() As MediaCollection
        If (myMediaCollectionSingleton Is Nothing) Then
            myMediaCollectionSingleton = New MediaCollection()
        End If

        Return myMediaCollectionSingleton
    End Function

    Private Sub New()
        Dim supportedAttributes() As String = {"author", "albumid", "title", "releasedateyear"}
        InitializeLibraryAndLookups(supportedAttributes)
    End Sub

    Private Sub InitializeLibraryAndLookups(ByVal supportedAttributes As String())
        Dim mediaPlayerCore As New WMPLib.WindowsMediaPlayerClass
        Dim mc As IWMPMediaCollection = mediaPlayerCore.mediaCollection
        Dim wmpplaylist As IWMPPlaylist = mc.getByAttribute("MediaType", "audio")
        Dim allTracksArray(wmpplaylist.count - 2) As Media

        'For index As Integer = 0 To wmpplaylist.count - 1 Step 1

        Parallel.For(0, wmpplaylist.count - 1,
         Sub(index As Integer)

             Dim mediaItem As New Media(wmpplaylist.Item(index))
             allTracksArray(index) = mediaItem
         End Sub)
        'Next

        For Each attributeName As String In supportedAttributes
            myAttributeLookup.Add(attributeName.ToLower.Trim, New Dictionary(Of String, ArrayList))
        Next

        allTracks = New ArrayList(allTracksArray)

        ' Parallel.For(0, allTracks.Count - 2,
        'Sub(index As Integer)

        For Each mediaItem As Media In allTracks
            'Dim mediaItem As IWMPMedia = DirectCast(allTracks.Item(index), IWMPMedia)
            For Each attributeName As String In supportedAttributes
                Try
                    Dim attributeValue As String = mediaItem.getItemInfo(attributeName).Trim.ToLower
                    Dim currentDictionary As Dictionary(Of String, ArrayList) = myAttributeLookup.Item(attributeName)


                    If (Not currentDictionary.ContainsKey(attributeValue)) Then
                        currentDictionary.Add(attributeValue, New ArrayList())
                    End If

                    If (currentDictionary.ContainsKey(attributeValue)) Then
                        currentDictionary.Item(attributeValue).Add(mediaItem)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Next
        Next
        'End Sub)

    End Sub


    Public Function getByAttribute(ByVal bstrAttribute As String, ByVal bstrValue As String) As Playlist
        Dim results As ArrayList = Nothing
        bstrAttribute = bstrAttribute.Trim.ToLower
        bstrValue = bstrValue.Trim.ToLower

        If (bstrAttribute = "mediatype" And bstrValue = "audio") Then
            results = allTracks
        Else
            If (myAttributeLookup.ContainsKey(bstrAttribute)) Then
                If (myAttributeLookup.Item(bstrAttribute).ContainsKey(bstrValue)) Then
                    results = myAttributeLookup.Item(bstrAttribute).Item(bstrValue)
                End If
            End If
        End If

        If (Not results Is Nothing) Then
            Dim resultPlaylist As New Playlist(results)
            Return resultPlaylist
        End If

        Return Nothing
    End Function

    Public Function GetPlaylistByQuery(ByRef theQuery As Query) As Playlist
        Return theQuery.ExtractMatchingPlaylist(Me)
    End Function


    Public ReadOnly Property AllMediaItems As Media()
        Get

            Return allTracks.ToArray(GetType(Media))
        End Get
    End Property


End Class
