Module modMain

    Sub Main()
        Dim lecture As IO.StreamReader, ligne As String, fichierPGN As String, fichierBAK As String, fichierTXT As String
        Dim joueurReference As String, joueurBlanc As String, joueurNoir As String
        Dim score As Single, nbScores As Integer, sommeGroupe As Single, groupe As Integer
        Dim totalScore As Single, elo As Single, question As Boolean
        Dim categories As String, chaineGroupe As String, chaineElo As String

        fichierPGN = Replace(Command(), """", "")
        If Not My.Computer.FileSystem.FileExists(fichierPGN) Then
            End
        Else
            fichierBAK = Replace(fichierPGN, ".pgn", "_bak.pgn")
            If My.Computer.FileSystem.FileExists(fichierBAK) Then
                My.Computer.FileSystem.DeleteFile(fichierBAK)
            End If
            My.Computer.FileSystem.CopyFile(fichierPGN, fichierBAK)
        End If
        lecture = New IO.StreamReader(fichierBAK)

        question = False
        
        nbScores = 0
        score = 0
        sommeGroupe = 0
        categories = ""
        chaineGroupe = ""
        totalScore = 0
        chaineElo = ""
        joueurBlanc = ""
        joueurNoir = ""
        joueurReference = ""

        Do
            ligne = lecture.ReadLine
            If InStr(ligne, "[White ", CompareMethod.Text) > 0 Then
                joueurBlanc = Replace(Replace(ligne, "[White """, ""), """]", "")
                joueurBlanc = Trim(LCase(joueurBlanc))
            ElseIf InStr(ligne, "[Black ", CompareMethod.Text) > 0 Then
                joueurNoir = Replace(Replace(ligne, "[Black """, ""), """]", "")
                joueurNoir = Trim(LCase(joueurNoir))
            ElseIf InStr(ligne, "[Result ", CompareMethod.Text) > 0 Then
                If Not question Then
                    If InStr(joueurBlanc, "experience", CompareMethod.Text) > 0 Then
                        joueurReference = InputBox("Enter the name of the reference player :", , joueurBlanc)
                    Else
                        joueurReference = InputBox("Enter the name of the reference player :", , joueurNoir)
                    End If
                    If joueurReference = "" Then
                        lecture.Close()
                        If My.Computer.FileSystem.FileExists(fichierBAK) Then
                            My.Computer.FileSystem.DeleteFile(fichierBAK)
                        End If
                        End
                    End If
                    joueurReference = Trim(LCase(joueurReference))

                    groupe = CInt(InputBox("Enter score group size :", , "10"))
                    If groupe <= 0 Then
                        lecture.Close()
                        If My.Computer.FileSystem.FileExists(fichierBAK) Then
                            My.Computer.FileSystem.DeleteFile(fichierBAK)
                        End If
                        End
                    End If
                    question = True
                End If

                If joueurBlanc = joueurReference Or joueurNoir = joueurReference Then
                    Select Case Replace(Replace(ligne, "[Result """, ""), """]", "")
                        Case "1/2-1/2"
                            score = 0.5

                        Case "1-0"
                            If joueurNoir = joueurReference Then
                                score = 0
                            Else
                                score = 1
                            End If

                        Case "0-1"
                            If joueurNoir = joueurReference Then
                                score = 1
                            Else
                                score = 0
                            End If
                    End Select
                    totalScore = totalScore + score
                    sommeGroupe = sommeGroupe + score
                    nbScores = nbScores + 1

                    'toutes les 10 (=groupe) parties ont réinitialise la somme
                    'on recalcule le elo toutes les 10 (=groupe) parties
                    If nbScores Mod groupe = 0 Then
                        categories = categories & nbScores & ";"
                        chaineGroupe = chaineGroupe & Format(sommeGroupe, "0.0") & ";"
                        sommeGroupe = 0

                        elo = 400 * Math.Log((totalScore / nbScores) / (1 - (totalScore / nbScores)), 10)
                        chaineElo = chaineElo & Format(elo, "0.0") & ";"
                    End If
                End If
            End If

        Loop Until lecture.EndOfStream
        lecture.Close()

        fichierTXT = My.Application.Info.DirectoryPath & "\scores.txt"

        My.Computer.FileSystem.WriteAllText(fichierTXT, categories & vbCrLf, False)
        My.Computer.FileSystem.WriteAllText(fichierTXT, chaineGroupe & vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(fichierTXT, chaineElo & vbCrLf, True)

        System.Diagnostics.Process.Start(fichierTXT)

        If My.Computer.FileSystem.FileExists(fichierBAK) Then
            My.Computer.FileSystem.DeleteFile(fichierBAK)
        End If

    End Sub

End Module
