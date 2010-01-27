﻿Imports System.IO
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager

    Public Class PlaylistModifierUILiason
        Private myDisplayName As String
        Private myAction As IModifierAction
        Private myFilePath As String
        Private myInputs As PlaylistModifierInput()
        Private myType As ModifierType

        Public Sub New(ByVal modifierFilePath As String)
            Load(modifierFilePath)
        End Sub

        Public Sub New(ByVal toCopy As PlaylistModifierUILiason)
            myDisplayName = toCopy.DisplayName
            myAction = toCopy.myAction
            myFilePath = toCopy.myFilePath

            Dim inputsList As ArrayList = New ArrayList()

            For Each input As PlaylistModifierInput In toCopy.myInputs
                inputsList.Add(New PlaylistModifierInput(input.DisplayName, input.ID, input.Value))
            Next

            myInputs = inputsList.ToArray(GetType(PlaylistModifierInput))
            myType = toCopy.myType
        End Sub

        Private Sub Load(ByRef modifierFilePath As String)
            If (File.Exists(modifierFilePath)) Then
                Dim modifierFile As XmlDocument = New XmlDocument()
                modifierFile.Load(modifierFilePath)
                myDisplayName = PlaylistManager.XMLGetValueAt(modifierFile, "//UI/DisplayName", 0)
                myAction = ConvertToAction(PlaylistManager.XMLGetValueAt(modifierFile, "//PlaylistModifier/Action", 0))
                Dim index As Integer = 0
                Dim inputs As ArrayList = New ArrayList()

                Dim inputLabel As String = PlaylistManager.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/UILabel", index)
                Dim inputID As String = PlaylistManager.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/ID", index)

                While (Not (inputLabel Is Nothing) And Not (inputID Is Nothing))
                    inputs.Add(New PlaylistModifierInput(inputLabel, inputID))
                    index += 1
                    inputLabel = PlaylistManager.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/UILabel", index)
                    inputID = PlaylistManager.XMLGetValueAt(modifierFile, "//UI/Inputs/Input/ID", index)
                End While

                myFilePath = modifierFilePath
                myInputs = inputs.ToArray(GetType(PlaylistModifierInput))
                myType = TranslateModifierType(PlaylistManager.XMLGetValueAt(modifierFile, "//PlaylistModifier/Type", 0))

            End If

        End Sub

        Public ReadOnly Property Type As ModifierType
            Get
                Return myType
            End Get
        End Property

        Private Function TranslateModifierType(ByVal typeName As String) As ModifierType
            If (typeName = "WMPAttribute") Then
                Return ModifierType.WMPAttribute
            ElseIf (typeName = "LastFM") Then
                Return ModifierType.LastFM
            End If

            Return Nothing
        End Function

        Private Function ConvertToAction(ByVal actionName As String) As IModifierAction
            If (actionName = "Add") Then
                Return New ModifierAction_Add
            ElseIf (actionName = "Subtract") Then
                Return New ModifierAction_Subtract
            ElseIf (actionName = "Filter") Then
                Return New ModifierAction_Filter
            End If

            Return Nothing
        End Function

        Public Property Inputs As PlaylistModifierInput()
            Get
                Return myInputs
            End Get
            Set(ByVal value As PlaylistModifierInput())
                myInputs = value
            End Set

        End Property

        Public ReadOnly Property FilePath As String
            Get
                Return myFilePath
            End Get
        End Property

        Public ReadOnly Property DisplayName As String
            Get
                Return myDisplayName
            End Get
        End Property

        Public Property ModifierAction As IModifierAction
            Get
                Return myAction
            End Get
            Set(ByVal value As IModifierAction)
                myAction = value
            End Set
        End Property



    End Class

End Class