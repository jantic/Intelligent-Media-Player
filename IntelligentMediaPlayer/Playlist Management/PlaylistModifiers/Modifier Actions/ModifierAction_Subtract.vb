﻿Imports AxWMPLib
Imports WMPLib

Partial Public Class PlaylistManager

    Public Class ModifierAction_Subtract
        Implements IModifierAction

        Private Const myName As String = "Subtract"

        Public Sub New()

        End Sub

        Public Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, ByRef attributeLookupArray() As Dictionary(Of String, String)) Implements IModifierAction.ModifyPlaylist


            For Each attributeLookup As Dictionary(Of String, String) In attributeLookupArray
                Dim query As WMPLib.IWMPQuery = mediaCollection.createQuery()

                For Each attributeName As String In attributeLookup.Keys().ToArray()
                    query.addCondition(attributeName, "Equals", attributeLookup.Item(attributeName))
                Next

                Dim result As IWMPPlaylist = mediaCollection.getPlaylistByQuery(query, "audio", "", False)
                Dim quickMediaLookup As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia) 'for proper, fast subtraction

                For y As Integer = 0 To currentPlaylist.count - 1 Step 1
                    If (y >= currentPlaylist.count) Then 'need to check since we're potentially removing items.
                        Exit For
                    End If

                    Dim item As IWMPMedia = currentPlaylist.Item(y)
                    Dim key As String = GenerateMediaHashKey(item)

                    If (Not (quickMediaLookup.ContainsKey(key))) Then
                        quickMediaLookup.Add(key, item)
                    Else
                        currentPlaylist.removeItem(item)
                        y -= 1
                    End If
                Next

                For index As Integer = 0 To result.count - 1 Step 1
                    Dim mediaItem As IWMPMedia = result.Item(index)
                    Dim key As String = GenerateMediaHashKey(mediaItem)

                    If (quickMediaLookup.ContainsKey(key)) Then
                        currentPlaylist.removeItem(quickMediaLookup(GenerateMediaHashKey(mediaItem)))
                        quickMediaLookup.Remove(key)
                    End If

                Next
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

                If (Not (quickMediaLookup.ContainsKey(key))) Then
                    quickMediaLookup.Add(key, item)
                Else
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