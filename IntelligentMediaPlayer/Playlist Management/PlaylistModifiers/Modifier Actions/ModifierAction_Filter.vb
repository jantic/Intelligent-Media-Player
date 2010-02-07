Imports AxWMPLib
Imports WMPLib

Partial Public Class PlaylistManager

    Public Class ModifierAction_Filter
        Implements IModifierAction

        Private Const myName As String = "Filter"

        Public Sub New()

        End Sub

        Public Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, ByRef attributeLookupArray() As Dictionary(Of String, String)) Implements IModifierAction.ModifyPlaylist


            Dim quickMediaLookup As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia) 'for proper, fast subtraction

            For Each attributeLookup As Dictionary(Of String, String) In attributeLookupArray
                Dim query As WMPLib.IWMPQuery = mediaCollection.createQuery()

                For Each attributeName As String In attributeLookup.Keys().ToArray()
                    query.addCondition(attributeName, "Equals", attributeLookup.Item(attributeName))
                Next

                Dim result As IWMPPlaylist = mediaCollection.getPlaylistByQuery(query, "audio", "", False)


                For y As Integer = 0 To result.count - 1 Step 1
                    If (y >= currentPlaylist.count) Then 'need to check since we're potentially removing items.
                        Exit For
                    End If

                    Dim item As IWMPMedia = result.Item(y)
                    Dim key As String = GenerateMediaHashKey(item)

                    If (Not (quickMediaLookup.ContainsKey(key))) Then
                        quickMediaLookup.Add(key, item)
                    End If
                Next

            Next

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                If (index >= currentPlaylist.count) Then
                    Exit For
                End If

                Dim mediaItem As IWMPMedia = currentPlaylist.Item(index)
                Dim key As String = GenerateMediaHashKey(mediaItem)

                If (Not quickMediaLookup.ContainsKey(key)) Then
                    currentPlaylist.removeItem(mediaItem)
                    index -= 1
                Else
                    quickMediaLookup.Remove(key)
                End If

            Next
        End Sub

        Public Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, ByRef modifyingPlaylist As IWMPPlaylist) Implements IModifierAction.ModifyPlaylist
            Dim quickMediaLookup As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia) 'for proper, fast subtraction

            For index As Integer = 0 To modifyingPlaylist.count - 1 Step 1
                Dim mediaItem As IWMPMedia = modifyingPlaylist.Item(index)
                Dim key As String = GenerateMediaHashKey(mediaItem)
                If (Not quickMediaLookup.ContainsKey(key)) Then
                    quickMediaLookup.Add(key, mediaItem)
                End If
            Next

            For y As Integer = 0 To currentPlaylist.count - 1 Step 1
                If (y >= currentPlaylist.count) Then 'need to check since we're potentially removing items.
                    Exit For
                End If

                Dim item As IWMPMedia = currentPlaylist.Item(y)
                Dim key As String = GenerateMediaHashKey(item)

                If (Not quickMediaLookup.ContainsKey(key)) Then
                    currentPlaylist.removeItem(item)
                    y -= 1
                End If
            Next
        End Sub

        Public ReadOnly Property Name As String Implements IModifierAction.Name
            Get
                Return myName
            End Get
        End Property

    End Class

End Class
