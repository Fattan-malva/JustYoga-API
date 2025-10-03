Public Class frmClassSchedule
    Public isClassMode As String
    Dim DT As New DataTable
    Dim dtStudio As New DataTable
    Dim dtRoomType As New DataTable
    Dim isFirstLoad As Boolean


    Private Sub loadSCH()
        cekConection()
        Dim DA As SqlClient.SqlDataAdapter
        Dim varSDate As DateTime, varEDate As DateTime
        DA = New SqlClient.SqlDataAdapter("select * from MstPeriode where '" & Format(dtp1.Value, "yyyyMMdd") & "' >= convert(varchar(8),SDate,112)  and '" & Format(dtp1.Value, "yyyyMMdd") & "' <= convert(varchar(8),EDate,112) order by SDate ASC", CN)
        Dim dtPRD As New DataTable
        DA.Fill(dtPRD)
        DA.Dispose()
        If dtPRD.Rows.Count > 0 Then
            varSDate = dtPRD(0)("Sdate")
            varEDate = dtPRD(0)("Edate")
        Else
            CustomeMessage("Warning", "List of Periode", "Date Periode out of range, please create the periode before.")
            dtPRD.Dispose()
            dtPRD = Nothing
            Exit Sub
        End If
        dtPRD.Dispose()
        dtPRD = Nothing

        Label16.Text = "Monday" & vbCr & Format(varSDate, "dd MMM yyyy")
        Label17.Text = "Tuesday" & vbCr & Format(DateAdd(DateInterval.Day, 1, varSDate), "dd MMM yyyy")
        Label18.Text = "Wednesday" & vbCr & Format(DateAdd(DateInterval.Day, 2, varSDate), "dd MMM yyyy")
        Label19.Text = "Thursday" & vbCr & Format(DateAdd(DateInterval.Day, 3, varSDate), "dd MMM yyyy")
        Label20.Text = "Friday" & vbCr & Format(DateAdd(DateInterval.Day, 4, varSDate), "dd MMM yyyy")
        Label21.Text = "Saturday" & vbCr & Format(DateAdd(DateInterval.Day, 5, varSDate), "dd MMM yyyy")
        Label22.Text = "Sunday" & vbCr & Format(DateAdd(DateInterval.Day, 6, varSDate), "dd MMM yyyy")


        Dim strSQL As String
        strSQL = "SELECT StudioID,RoomType,YearPrd,WeekPrd,tcs.SDate,tcs.EDate,tcs.TimeCls," & _
          "MonTch1+'-'+mon1.employeeName as 'MonTch1'," & _
          "MonTch2+'-'+mon2.employeeName as 'MonTch2'," & _
          "MonCls+'-'+mon3.ClassName as 'MonCls',isnull(Date1,cast('1900-01-01' as smalldatetime)) as Date1," & _
          "TueTch1+'-'+tue1.employeeName as 'TueTch1'," & _
          "TueTch2+'-'+tue2.employeeName as 'TueTch2'," & _
          "TueCls+'-'+tue3.classname as 'TueCls',isnull(Date2,cast('1900-01-01' as smalldatetime)) as Date2," & _
          "WedTch1+'-'+wed1.employeeName as 'WedTch1'," & _
          "WedTch2+'-'+wed2.employeeName as 'WedTch2'," & _
          "WedCls+'-'+Wed3.classname as 'WedCls',isnull(Date3,cast('1900-01-01' as smalldatetime)) as Date3," & _
          "ThuTch1+'-'+thu1.employeeName as 'ThuTch1'," & _
          "ThuTch2+'-'+thu2.employeeName as 'ThuTch2'," & _
          "ThuCls+'-'+thu3.classname as 'ThuCls',isnull(Date4,cast('1900-01-01' as smalldatetime)) as Date4," & _
          "FriTch1+'-'+Fri1.employeeName as 'FriTch1'," & _
          "FriTch2+'-'+Fri2.employeeName as 'FriTch2'," & _
          "FriCls+'-'+Fri3.Classname as 'FriCls',isnull(Date5,cast('1900-01-01' as smalldatetime)) as Date5," & _
          "SatTch1+'-'+Sat1.employeeName as 'SatTch1'," & _
          "SatTch2+'-'+Sat2.employeeName as 'SatTch2'," & _
          "SatCls+'-'+Sat3.Classname as 'SatCls',isnull(Date6,cast('1900-01-01' as smalldatetime)) as Date6," & _
          "SunTch1+'-'+Sun1.employeeName as 'SunTch1'," & _
          "SunTch2+'-'+Sun2.employeeName as 'SunTch2'," & _
          "SunCls+'-'+Sun3.Classname as 'SunCls',isnull(Date7,cast('1900-01-01' as smalldatetime)) as Date7 " & _
            "FROM TrxClassSchedule tcs " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as mon1 on tcs.montch1=mon1.employeeID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as mon2 on tcs.montch2=mon2.employeeID " & _
        "left outer join (select ClassID,ClassName from MstClass where isActive=1) as mon3 on tcs.moncls=mon3.ClassID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as tue1 on tcs.tuetch1=tue1.employeeID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as tue2 on tcs.tuetch2=tue2.employeeID " & _
        "left outer join (select ClassID,ClassName from MstClass where isActive=1) as tue3 on tcs.tuecls=tue3.ClassID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as wed1 on tcs.wedtch1=wed1.employeeID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as wed2 on tcs.wedtch2=wed2.employeeID " & _
        "left outer join (select ClassID,ClassName from MstClass where isActive=1) as wed3 on tcs.wedcls=wed3.ClassID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as thu1 on tcs.thutch1=thu1.employeeID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as thu2 on tcs.thutch2=thu2.employeeID " & _
        "left outer join (select ClassID,ClassName from MstClass where isActive=1) as thu3 on tcs.thucls=thu3.ClassID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as fri1 on tcs.fritch1=fri1.employeeID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as fri2 on tcs.fritch2=fri2.employeeID " & _
        "left outer join (select ClassID,ClassName from MstClass where isActive=1) as fri3 on tcs.fricls=fri3.ClassID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as sat1 on tcs.sattch1=sat1.employeeID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as sat2 on tcs.sattch2=sat2.employeeID " & _
        "left outer join (select ClassID,ClassName from MstClass where isActive=1) as sat3 on tcs.satcls=sat3.ClassID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as sun1 on tcs.suntch1=sun1.employeeID " & _
        "left outer join (select employeeid,employeename from MstEmployee where active=1 and positionid in (" & pubTchJBT & ") and deptid in (" & pubTchDept & ") ) as sun2 on tcs.suntch2=sun2.employeeID " & _
        "left outer join (select ClassID,ClassName from MstClass where isActive=1) as sun3 on tcs.suncls=sun3.ClassID " & _
        "WHERE tcs.StudioID='" & cmbStudio.SelectedValue & "' AND tcs.RoomType='" & cmbRoomType.SelectedValue & "' " & _
        "AND  convert(varchar(8),SDate,112) = '" & Format(varSDate, "yyyyMMdd") & "' and convert(varchar(8),EDate,112) = '" & Format(varEDate, "yyyyMMdd") & "' " & _
        "order by tcs.timecls"

        'IIf(LCase(isClassMode) <> "uploading", " ", " inner join MstTimeSchedule mts on tcs.timecls=mts.stime ") & _

        DT.Dispose()
        DT = New DataTable
        DA = New SqlClient.SqlDataAdapter(strSQL, CN)
        DA.Fill(DT)
        DA.Dispose()

        '----Clear all Object in panel
        For Each oCTL As Control In Panel1.Controls
            oCTL.Text = "-"
            oCTL.Tag = ""
            'oCTL.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date1"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            'oCTL.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
        Next

        '---- fill the time for the first time
        Dim dtTime As New DataTable
        Dim strTime As String

        If LCase(isClassMode) <> "uploading" Then
            strTime = "select * from MstTimeSchedule mts " & _
                                              "inner join (" & _
                                                    "select distinct sdate,edate,timecls from TrxClassSchedule " & _
                                                    "where roomtype='" & cmbRoomType.SelectedValue & "' " & _
                                                    " and convert(varchar(8),SDate,112)='" & Format(varSDate, "yyyyMMdd") & "' " & _
                                                    " and convert(varchar(8),EDate,112) = '" & Format(varEDate, "yyyyMMdd") & "' " & _
                                                    ") tcs on mts.sdate=tcs.sdate and mts.edate=tcs.edate " & _
                                                "where mts.studioid='" & cmbStudio.SelectedValue & "' " & _
                                                "and convert(varchar(8),mts.SDate,112)='" & Format(varSDate, "yyyyMMdd") & "' " & _
                                                "and convert(varchar(8),mts.EDate,112) = '" & Format(varEDate, "yyyyMMdd") & "' " & _
                                                "and mts.stime=tcs.timecls " & _
                                                "order by mts.STime ASC"
        Else
            strTime = "select * from MstTimeSchedule " & _
                                            "where studioid='" & cmbStudio.SelectedValue & "' and convert(varchar(8),SDate,112)='" & Format(varSDate, "yyyyMMdd") & "' " & _
                                            "and convert(varchar(8),EDate,112) = '" & Format(varEDate, "yyyyMMdd") & "' " & _
                                            "order by STime ASC"
        End If

        DA = New SqlClient.SqlDataAdapter(strTime, CN)
        DA.Fill(dtTime)
        DA.Dispose()

        If dtTime.Rows.Count > 0 Then
            For a As Long = 1 To dtTime.Rows.Count
                For Each oLBL As Control In Panel1.Controls
                    If oLBL.GetType Is GetType(Label) Then
                        If oLBL.Name = "L" & Microsoft.VisualBasic.Right("00" + a.ToString.Trim, 2) & "_1" Then
                            'If dtTime(a)("stime") <> "" Or Not IsDBNull(dtTime(a)("stime")) Then
                            oLBL.Text = dtTime(a - 1)("stime") & vbCrLf & dtTime(a - 1)("etime")
                            oLBL.Tag = ""
                            'End If
                        End If
                    End If
                Next
            Next
        End If



        'If LCase(isClassMode) <> "uploading" And DT.Rows.Count = 0 Then Exit Sub

        '-------set date template for button
        Dim varGetTime As String = ""
        For x As Byte = 1 To 15
            For y As Byte = 1 To 7
                For Each oBtn As Control In Panel1.Controls
                    If oBtn.GetType Is GetType(Janus.Windows.EditControls.UIButton) Then
                        If oBtn.Name = "B" & Microsoft.VisualBasic.Right("00" + x.ToString.Trim, 2) & "_" & y Then

                            For Each oLBLT As Control In Panel1.Controls
                                If oLBLT.GetType Is GetType(Label) Then
                                    If oLBLT.Name = "L" & Microsoft.VisualBasic.Right("00" + x.ToString.Trim, 2) & "_1" Then
                                        varGetTime = oLBLT.Text
                                        Exit For
                                    End If
                                End If
                            Next

                            Select Case y
                                Case 1
                                    oBtn.Text = "-"
                                    oBtn.Tag = "kosong|" & Format(varSDate, "dd MMM yyyy") & "|" & varGetTime
                                Case 2
                                    oBtn.Text = "-"
                                    oBtn.Tag = "kosong|" & Format(DateAdd("d", 1, varSDate), "dd MMM yyyy") & "|" & varGetTime
                                Case 3
                                    oBtn.Text = "-"
                                    oBtn.Tag = "kosong|" & Format(DateAdd("d", 2, varSDate), "dd MMM yyyy") & "|" & varGetTime
                                Case 4
                                    oBtn.Text = "-"
                                    oBtn.Tag = "kosong|" & Format(DateAdd("d", 3, varSDate), "dd MMM yyyy") & "|" & varGetTime
                                Case 5
                                    oBtn.Text = "-"
                                    oBtn.Tag = "kosong|" & Format(DateAdd("d", 4, varSDate), "dd MMM yyyy") & "|" & varGetTime
                                Case 6
                                    oBtn.Text = "-"
                                    oBtn.Tag = "kosong|" & Format(DateAdd("d", 5, varSDate), "dd MMM yyyy") & "|" & varGetTime
                                Case 7
                                    oBtn.Text = "-"
                                    oBtn.Tag = "kosong|" & Format(DateAdd("d", 6, varSDate), "dd MMM yyyy") & "|" & varGetTime
                            End Select

                            Exit For
                        End If
                    End If
                Next
            Next
        Next


        '---------fill button with record
        For i As Long = 1 To DT.Rows.Count


            For c As Byte = 1 To 15
                For Each oLBL As Control In Panel1.Controls
                    If oLBL.GetType Is GetType(Label) Then
                        If oLBL.Name = "L" & Microsoft.VisualBasic.Right("00" + c.ToString.Trim, 2) & "_" & 1 Then
                            Dim arrTime() As String = Split(oLBL.Text, vbCrLf)
                            If arrTime(0).Trim = DT(i - 1)("TIMECLS").ToString.Trim Then

                                For j As Byte = 1 To 7
                                    For Each oBtn As Control In Panel1.Controls
                                        If oBtn.GetType Is GetType(Janus.Windows.EditControls.UIButton) Then
                                            If oBtn.Name = "B" & Microsoft.VisualBasic.Right("00" + c.ToString.Trim, 2) & "_" & j Then
                                                Select Case j
                                                    Case 1
                                                        Dim tch1() As String = Split(DT(i - 1)("montch1").ToString.Trim, "-")
                                                        Dim tch2() As String = Split(DT(i - 1)("montch2").ToString.Trim, "-")
                                                        Dim ClsID() As String = Split(DT(i - 1)("moncls").ToString.Trim, "-")

                                                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
                                                        If tch1(0) = "" Then
                                                            sTchName1 = ""
                                                        Else
                                                            sTchName1 = tch1(1)
                                                        End If

                                                        If tch2(0) = "" Then
                                                            sTchName2 = ""
                                                        Else
                                                            sTchName2 = tch2(1)
                                                        End If

                                                        If ClsID(0) = "" Then
                                                            sClassName = ""
                                                        Else
                                                            sClassName = ClsID(1)
                                                        End If

                                                        'oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
                                                        oBtn.Text = sClassName & vbCrLf & sTchName1 & " - " & sTchName2
                                                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
                                                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date1"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
                                                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
                                                        End If

                                                    Case 2
                                                        Dim tch1() As String = Split(DT(i - 1)("tuetch1").ToString.Trim, "-")
                                                        Dim tch2() As String = Split(DT(i - 1)("tuetch2").ToString.Trim, "-")
                                                        Dim ClsID() As String = Split(DT(i - 1)("tuecls").ToString.Trim, "-")
                                                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
                                                        If tch1(0) = "" Then
                                                            sTchName1 = ""
                                                        Else
                                                            sTchName1 = tch1(1)
                                                        End If

                                                        If tch2(0) = "" Then
                                                            sTchName2 = ""
                                                        Else
                                                            sTchName2 = tch2(1)
                                                        End If

                                                        If ClsID(0) = "" Then
                                                            sClassName = ""
                                                        Else
                                                            sClassName = ClsID(1)
                                                        End If

                                                        'oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
                                                        oBtn.Text = sClassName & vbCrLf & sTchName1 & " - " & sTchName2
                                                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
                                                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date2"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
                                                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim

                                                        End If

                                                    Case 3
                                                        Dim tch1() As String = Split(DT(i - 1)("wedtch1").ToString.Trim, "-")
                                                        Dim tch2() As String = Split(DT(i - 1)("wedtch2").ToString.Trim, "-")
                                                        Dim ClsID() As String = Split(DT(i - 1)("wedcls").ToString.Trim, "-")
                                                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
                                                        If tch1(0) = "" Then
                                                            sTchName1 = ""
                                                        Else
                                                            sTchName1 = tch1(1)
                                                        End If

                                                        If tch2(0) = "" Then
                                                            sTchName2 = ""
                                                        Else
                                                            sTchName2 = tch2(1)
                                                        End If

                                                        If ClsID(0) = "" Then
                                                            sClassName = ""
                                                        Else
                                                            sClassName = ClsID(1)
                                                        End If

                                                        'oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
                                                        oBtn.Text = sClassName & vbCrLf & sTchName1 & " - " & sTchName2
                                                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
                                                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date3"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
                                                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
                                                        End If

                                                    Case 4
                                                        Dim tch1() As String = Split(DT(i - 1)("thutch1").ToString.Trim, "-")
                                                        Dim tch2() As String = Split(DT(i - 1)("thutch2").ToString.Trim, "-")
                                                        Dim ClsID() As String = Split(DT(i - 1)("thucls").ToString.Trim, "-")
                                                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
                                                        If tch1(0) = "" Then
                                                            sTchName1 = ""
                                                        Else
                                                            sTchName1 = tch1(1)
                                                        End If

                                                        If tch2(0) = "" Then
                                                            sTchName2 = ""
                                                        Else
                                                            sTchName2 = tch2(1)
                                                        End If

                                                        If ClsID(0) = "" Then
                                                            sClassName = ""
                                                        Else
                                                            sClassName = ClsID(1)
                                                        End If

                                                        'oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
                                                        oBtn.Text = sClassName & vbCrLf & sTchName1 & " - " & sTchName2
                                                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
                                                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date4"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
                                                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
                                                        End If

                                                    Case 5
                                                        Dim tch1() As String = Split(DT(i - 1)("fritch1").ToString.Trim, "-")
                                                        Dim tch2() As String = Split(DT(i - 1)("fritch2").ToString.Trim, "-")
                                                        Dim ClsID() As String = Split(DT(i - 1)("fricls").ToString.Trim, "-")
                                                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
                                                        If tch1(0) = "" Then
                                                            sTchName1 = ""
                                                        Else
                                                            sTchName1 = tch1(1)
                                                        End If

                                                        If tch2(0) = "" Then
                                                            sTchName2 = ""
                                                        Else
                                                            sTchName2 = tch2(1)
                                                        End If

                                                        If ClsID(0) = "" Then
                                                            sClassName = ""
                                                        Else
                                                            sClassName = ClsID(1)
                                                        End If

                                                        'oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
                                                        oBtn.Text = sClassName & vbCrLf & sTchName1 & " - " & sTchName2
                                                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
                                                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date5"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
                                                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
                                                        End If

                                                    Case 6
                                                        Dim tch1() As String = Split(DT(i - 1)("sattch1").ToString.Trim, "-")
                                                        Dim tch2() As String = Split(DT(i - 1)("sattch2").ToString.Trim, "-")
                                                        Dim ClsID() As String = Split(DT(i - 1)("satcls").ToString.Trim, "-")
                                                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
                                                        If tch1(0) = "" Then
                                                            sTchName1 = ""
                                                        Else
                                                            sTchName1 = tch1(1)
                                                        End If

                                                        If tch2(0) = "" Then
                                                            sTchName2 = ""
                                                        Else
                                                            sTchName2 = tch2(1)
                                                        End If

                                                        If ClsID(0) = "" Then
                                                            sClassName = ""
                                                        Else
                                                            sClassName = ClsID(1)
                                                        End If

                                                        'oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
                                                        oBtn.Text = sClassName & vbCrLf & sTchName1 & " - " & sTchName2
                                                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
                                                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date6"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
                                                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
                                                        End If

                                                    Case 7
                                                        Dim tch1() As String = Split(DT(i - 1)("suntch1").ToString.Trim, "-")
                                                        Dim tch2() As String = Split(DT(i - 1)("suntch2").ToString.Trim, "-")
                                                        Dim ClsID() As String = Split(DT(i - 1)("suncls").ToString.Trim, "-")
                                                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
                                                        If tch1(0) = "" Then
                                                            sTchName1 = ""
                                                        Else
                                                            sTchName1 = tch1(1)
                                                        End If

                                                        If tch2(0) = "" Then
                                                            sTchName2 = ""
                                                        Else
                                                            sTchName2 = tch2(1)
                                                        End If

                                                        If ClsID(0) = "" Then
                                                            sClassName = ""
                                                        Else
                                                            sClassName = ClsID(1)
                                                        End If

                                                        'oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
                                                        oBtn.Text = sClassName & vbCrLf & sTchName1 & " - " & sTchName2
                                                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
                                                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date7"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
                                                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
                                                        End If

                                                End Select

                                                Exit For
                                            End If
                                        End If
                                    Next
                                Next

                            End If
                        End If
                    End If
                Next
            Next




            '' '' ''------ real code
            ' '' ''For Each oLBL As Control In Panel1.Controls
            ' '' ''    If oLBL.GetType Is GetType(Label) Then
            ' '' ''        If oLBL.Name = "L" & Microsoft.VisualBasic.Right("00" + i.ToString.Trim, 2) & "_" & 1 Then
            ' '' ''            oLBL.Text = DT(i - 1)("TIMECLS").ToString.Trim
            ' '' ''        End If
            ' '' ''    End If
            ' '' ''Next

            ' '' ''For j As Byte = 1 To 7
            ' '' ''    For Each oBtn As Control In Panel1.Controls
            ' '' ''        If oBtn.GetType Is GetType(Janus.Windows.EditControls.UIButton) Then
            ' '' ''            If oBtn.Name = "B" & Microsoft.VisualBasic.Right("00" + i.ToString.Trim, 2) & "_" & j Then
            ' '' ''                Select Case j
            ' '' ''                    Case 1
            ' '' ''                        Dim tch1() As String = Split(DT(i - 1)("montch1").ToString.Trim, "-")
            ' '' ''                        Dim tch2() As String = Split(DT(i - 1)("montch2").ToString.Trim, "-")
            ' '' ''                        Dim ClsID() As String = Split(DT(i - 1)("moncls").ToString.Trim, "-")

            ' '' ''                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
            ' '' ''                        If tch1(0) = "" Then
            ' '' ''                            sTchName1 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName1 = tch1(1)
            ' '' ''                        End If

            ' '' ''                        If tch2(0) = "" Then
            ' '' ''                            sTchName2 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName2 = tch2(1)
            ' '' ''                        End If

            ' '' ''                        If ClsID(0) = "" Then
            ' '' ''                            sClassName = ""
            ' '' ''                        Else
            ' '' ''                            sClassName = ClsID(1)
            ' '' ''                        End If

            ' '' ''                        oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
            ' '' ''                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
            ' '' ''                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date1"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            ' '' ''                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
            ' '' ''                        End If

            ' '' ''                    Case 2
            ' '' ''                        Dim tch1() As String = Split(DT(i - 1)("tuetch1").ToString.Trim, "-")
            ' '' ''                        Dim tch2() As String = Split(DT(i - 1)("tuetch2").ToString.Trim, "-")
            ' '' ''                        Dim ClsID() As String = Split(DT(i - 1)("tuecls").ToString.Trim, "-")
            ' '' ''                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
            ' '' ''                        If tch1(0) = "" Then
            ' '' ''                            sTchName1 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName1 = tch1(1)
            ' '' ''                        End If

            ' '' ''                        If tch2(0) = "" Then
            ' '' ''                            sTchName2 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName2 = tch2(1)
            ' '' ''                        End If

            ' '' ''                        If ClsID(0) = "" Then
            ' '' ''                            sClassName = ""
            ' '' ''                        Else
            ' '' ''                            sClassName = ClsID(1)
            ' '' ''                        End If

            ' '' ''                        oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
            ' '' ''                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
            ' '' ''                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date2"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            ' '' ''                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
            ' '' ''                        End If

            ' '' ''                    Case 3
            ' '' ''                        Dim tch1() As String = Split(DT(i - 1)("wedtch1").ToString.Trim, "-")
            ' '' ''                        Dim tch2() As String = Split(DT(i - 1)("wedtch2").ToString.Trim, "-")
            ' '' ''                        Dim ClsID() As String = Split(DT(i - 1)("wedcls").ToString.Trim, "-")
            ' '' ''                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
            ' '' ''                        If tch1(0) = "" Then
            ' '' ''                            sTchName1 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName1 = tch1(1)
            ' '' ''                        End If

            ' '' ''                        If tch2(0) = "" Then
            ' '' ''                            sTchName2 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName2 = tch2(1)
            ' '' ''                        End If

            ' '' ''                        If ClsID(0) = "" Then
            ' '' ''                            sClassName = ""
            ' '' ''                        Else
            ' '' ''                            sClassName = ClsID(1)
            ' '' ''                        End If

            ' '' ''                        oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
            ' '' ''                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
            ' '' ''                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date3"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            ' '' ''                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
            ' '' ''                        End If

            ' '' ''                    Case 4
            ' '' ''                        Dim tch1() As String = Split(DT(i - 1)("thutch1").ToString.Trim, "-")
            ' '' ''                        Dim tch2() As String = Split(DT(i - 1)("thutch2").ToString.Trim, "-")
            ' '' ''                        Dim ClsID() As String = Split(DT(i - 1)("thucls").ToString.Trim, "-")
            ' '' ''                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
            ' '' ''                        If tch1(0) = "" Then
            ' '' ''                            sTchName1 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName1 = tch1(1)
            ' '' ''                        End If

            ' '' ''                        If tch2(0) = "" Then
            ' '' ''                            sTchName2 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName2 = tch2(1)
            ' '' ''                        End If

            ' '' ''                        If ClsID(0) = "" Then
            ' '' ''                            sClassName = ""
            ' '' ''                        Else
            ' '' ''                            sClassName = ClsID(1)
            ' '' ''                        End If

            ' '' ''                        oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
            ' '' ''                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
            ' '' ''                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date4"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            ' '' ''                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
            ' '' ''                        End If

            ' '' ''                    Case 5
            ' '' ''                        Dim tch1() As String = Split(DT(i - 1)("fritch1").ToString.Trim, "-")
            ' '' ''                        Dim tch2() As String = Split(DT(i - 1)("fritch2").ToString.Trim, "-")
            ' '' ''                        Dim ClsID() As String = Split(DT(i - 1)("fricls").ToString.Trim, "-")
            ' '' ''                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
            ' '' ''                        If tch1(0) = "" Then
            ' '' ''                            sTchName1 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName1 = tch1(1)
            ' '' ''                        End If

            ' '' ''                        If tch2(0) = "" Then
            ' '' ''                            sTchName2 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName2 = tch2(1)
            ' '' ''                        End If

            ' '' ''                        If ClsID(0) = "" Then
            ' '' ''                            sClassName = ""
            ' '' ''                        Else
            ' '' ''                            sClassName = ClsID(1)
            ' '' ''                        End If

            ' '' ''                        oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
            ' '' ''                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
            ' '' ''                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date5"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            ' '' ''                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
            ' '' ''                        End If

            ' '' ''                    Case 6
            ' '' ''                        Dim tch1() As String = Split(DT(i - 1)("sattch1").ToString.Trim, "-")
            ' '' ''                        Dim tch2() As String = Split(DT(i - 1)("sattch2").ToString.Trim, "-")
            ' '' ''                        Dim ClsID() As String = Split(DT(i - 1)("satcls").ToString.Trim, "-")
            ' '' ''                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
            ' '' ''                        If tch1(0) = "" Then
            ' '' ''                            sTchName1 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName1 = tch1(1)
            ' '' ''                        End If

            ' '' ''                        If tch2(0) = "" Then
            ' '' ''                            sTchName2 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName2 = tch2(1)
            ' '' ''                        End If

            ' '' ''                        If ClsID(0) = "" Then
            ' '' ''                            sClassName = ""
            ' '' ''                        Else
            ' '' ''                            sClassName = ClsID(1)
            ' '' ''                        End If

            ' '' ''                        oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
            ' '' ''                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
            ' '' ''                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date6"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            ' '' ''                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
            ' '' ''                        End If

            ' '' ''                    Case 7
            ' '' ''                        Dim tch1() As String = Split(DT(i - 1)("suntch1").ToString.Trim, "-")
            ' '' ''                        Dim tch2() As String = Split(DT(i - 1)("suntch2").ToString.Trim, "-")
            ' '' ''                        Dim ClsID() As String = Split(DT(i - 1)("suncls").ToString.Trim, "-")
            ' '' ''                        Dim sTchName1 As String, sTchName2 As String, sClassName As String
            ' '' ''                        If tch1(0) = "" Then
            ' '' ''                            sTchName1 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName1 = tch1(1)
            ' '' ''                        End If

            ' '' ''                        If tch2(0) = "" Then
            ' '' ''                            sTchName2 = ""
            ' '' ''                        Else
            ' '' ''                            sTchName2 = tch2(1)
            ' '' ''                        End If

            ' '' ''                        If ClsID(0) = "" Then
            ' '' ''                            sClassName = ""
            ' '' ''                        Else
            ' '' ''                            sClassName = ClsID(1)
            ' '' ''                        End If

            ' '' ''                        oBtn.Text = sTchName1 & " - " & sTchName2 & vbCrLf & sClassName
            ' '' ''                        If oBtn.Text <> "" And oBtn.Text.Trim <> "-" Then
            ' '' ''                            oBtn.Tag = DT(i - 1)("StudioID").ToString.Trim & "|" & DT(i - 1)("RoomType").ToString.Trim & "|" & Format(DT(i - 1)("Date7"), "dd MMM yyyy") & "|" & DT(i - 1)("timecls").ToString.Trim & "|"
            ' '' ''                            oBtn.Tag &= tch1(0).Trim & "|" & IIf(tch2(0).Trim = "", "0", tch2(0).Trim) & "|" & ClsID(0).Trim
            ' '' ''                        End If

            ' '' ''                End Select

            ' '' ''                Exit For
            ' '' ''            End If
            ' '' ''        End If
            ' '' ''    Next
            ' '' ''Next
        Next


    End Sub

    Private Sub loadCombo()
        dtStudio.Dispose()
        dtStudio = New DataTable
        dtRoomType.Dispose()
        dtRoomType = New DataTable

        Dim DA As New SqlClient.SqlDataAdapter("select StudioID,Name as StudioName from MstStudio order by name", CN)
        DA.Fill(dtStudio)
        DA.Dispose()

        DA = New SqlClient.SqlDataAdapter("select RoomType,RoomName from MstRoomType order by RoomName", CN)
        DA.Fill(dtRoomType)
        DA.Dispose()

        cmbStudio.Items.Clear()
        cmbStudio.DataSource = dtStudio
        cmbStudio.ValueMember = dtStudio.Columns("StudioID").ToString
        cmbStudio.DisplayMember = dtStudio.Columns("StudioName").ToString

        cmbRoomType.Items.Clear()
        cmbRoomType.DataSource = dtRoomType
        cmbRoomType.ValueMember = dtRoomType.Columns("RoomType").ToString
        cmbRoomType.DisplayMember = dtRoomType.Columns("RoomName").ToString


    End Sub

    Private Sub frmClassSchedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'lblDateTimeHDR.Text = "Date" & vbCrLf & "Time"
        isFirstLoad = True
        dtp1.Value = Date.Now
        cekConection()
        lblDateNow.Text = Format(Date.Now, "dddd [dd MMM yyyy]")
        loadCombo()

        cmbStudio.SelectedValue = studioID
        cmbRoomType.SelectedValue = 0

        'loadSCH()

        If LCase(isClassMode) = "schedule" Or LCase(isClassMode) = "uploading" Then
            dtp1.Visible = True
        Else
            dtp1.Visible = True
        End If

        'isClassMode = "booking"
        'isClassMode = "uploading"

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CreateClassBooking(ByVal strText As String, ByVal strTag As String)
        Select Case LCase(isClassMode)
            Case "booking"
                Dim zArr() As String = Split(strTag, "|")
                If Format(CDate(zArr(2)), "yyyyMMdd") < Format(Date.Now, "yyyyMMdd") Then
                    CustomeMessage("", "Invalid Booking Date", "Booking Date must be greater than or on the same date with today")
                    Exit Sub
                Else
                    doBooking(strText, strTag)
                End If

            Case "uploading"
                doUploading(strText, strTag)

        End Select
    End Sub

    Private Sub doUploading(ByVal strText As String, ByVal strTag As String)
        Dim arrTemp() As String = Split(strTag, "|")

        'If arrTemp.Length = 1 Then Exit Sub

        If arrTemp.Length = 3 Then
            frmClassBookingUpload._pubStudio = cmbStudio.SelectedValue
            frmClassBookingUpload._pubRoomType = cmbRoomType.SelectedValue
            frmClassBookingUpload._pubDate = arrTemp(1)
            frmClassBookingUpload._pubTimeCls = arrTemp(2)
            frmClassBookingUpload._pubTch1 = ""
            frmClassBookingUpload._pubTch2 = ""
            frmClassBookingUpload._pubClassID = "0"
        Else
            frmClassBookingUpload._pubStudio = arrTemp(0)
            frmClassBookingUpload._pubRoomType = arrTemp(1)
            frmClassBookingUpload._pubDate = arrTemp(2)
            frmClassBookingUpload._pubTimeCls = arrTemp(3)
            frmClassBookingUpload._pubTch1 = arrTemp(4)
            frmClassBookingUpload._pubTch2 = arrTemp(5)
            frmClassBookingUpload._pubClassID = arrTemp(6)
        End If
        frmClassBookingUpload.ShowDialog()
        loadSCH()
        'MessageBox.Show(strTag)
    End Sub

    Private Sub doBooking(ByVal strText As String, ByVal strTag As String)
        Dim arrTemp() As String = Split(strTag, "|")
        Dim arrClass() As String = Split(strText, vbCrLf)

        If arrTemp.Length < 7 Then
        Else
            frmClassBooking._pubText = strText
            frmClassBooking._pubTag = strTag
            frmClassBooking._pubClassNameID = cmbRoomType.Text & " *** " & arrTemp(3)   'arrTemp(6) & "-" & arrClass(1) & "-" & arrTemp(3)
            frmClassBooking.ShowDialog()
            frmClassBooking.Dispose()

        End If

    End Sub


    Private Sub B01_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B01_1.Click
        CreateClassBooking(B01_1.Text, B01_1.Tag)
    End Sub
    Private Sub B15_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B15_6.Click
        CreateClassBooking(B15_6.Text, B15_6.Tag)
    End Sub
    Private Sub B15_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B15_5.Click
        CreateClassBooking(B15_5.Text, B15_5.Tag)
    End Sub
    Private Sub B15_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B15_4.Click
        CreateClassBooking(B15_4.Text, B15_4.Tag)
    End Sub
    Private Sub B15_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B15_3.Click
        CreateClassBooking(B15_3.Text, B15_3.Tag)
    End Sub
    Private Sub B15_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B15_2.Click
        CreateClassBooking(B15_2.Text, B15_2.Tag)
    End Sub
    Private Sub B15_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B15_1.Click
        CreateClassBooking(B15_1.Text, B15_1.Tag)
    End Sub
    Private Sub B14_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B14_7.Click
        CreateClassBooking(B14_7.Text, B14_7.Tag)
    End Sub
    Private Sub B14_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B14_6.Click
        CreateClassBooking(B14_6.Text, B14_6.Tag)
    End Sub
    Private Sub B14_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B14_5.Click
        CreateClassBooking(B14_5.Text, B14_5.Tag)
    End Sub
    Private Sub B14_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B14_4.Click
        CreateClassBooking(B14_4.Text, B14_4.Tag)
    End Sub
    Private Sub B14_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B14_3.Click
        CreateClassBooking(B14_3.Text, B14_3.Tag)
    End Sub
    Private Sub B14_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B14_2.Click
        CreateClassBooking(B14_2.Text, B14_2.Tag)
    End Sub
    Private Sub B14_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B14_1.Click
        CreateClassBooking(B14_1.Text, B14_1.Tag)
    End Sub
    Private Sub B13_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B13_7.Click
        CreateClassBooking(B13_7.Text, B13_7.Tag)
    End Sub
    Private Sub B13_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B13_6.Click
        CreateClassBooking(B13_6.Text, B13_6.Tag)
    End Sub
    Private Sub B13_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B13_5.Click
        CreateClassBooking(B13_5.Text, B13_5.Tag)
    End Sub
    Private Sub B13_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B13_4.Click
        CreateClassBooking(B13_4.Text, B13_4.Tag)
    End Sub
    Private Sub B13_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B13_3.Click
        CreateClassBooking(B13_3.Text, B13_3.Tag)
    End Sub
    Private Sub B13_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B13_2.Click
        CreateClassBooking(B13_2.Text, B13_2.Tag)
    End Sub
    Private Sub B13_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B13_1.Click
        CreateClassBooking(B13_1.Text, B13_1.Tag)
    End Sub
    Private Sub B12_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B12_7.Click
        CreateClassBooking(B12_7.Text, B12_7.Tag)
    End Sub
    Private Sub B12_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B12_6.Click
        CreateClassBooking(B12_6.Text, B12_6.Tag)
    End Sub
    Private Sub B12_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B12_5.Click
        CreateClassBooking(B12_5.Text, B12_5.Tag)
    End Sub
    Private Sub B12_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B12_4.Click
        CreateClassBooking(B12_4.Text, B12_4.Tag)
    End Sub
    Private Sub B12_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B12_3.Click
        CreateClassBooking(B12_3.Text, B12_3.Tag)
    End Sub
    Private Sub B12_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B12_2.Click
        CreateClassBooking(B12_2.Text, B12_2.Tag)
    End Sub
    Private Sub B12_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B12_1.Click
        CreateClassBooking(B12_1.Text, B12_1.Tag)
    End Sub
    Private Sub B11_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B11_7.Click
        CreateClassBooking(B11_7.Text, B11_7.Tag)
    End Sub
    Private Sub B11_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B11_6.Click
        CreateClassBooking(B11_6.Text, B11_6.Tag)
    End Sub
    Private Sub B11_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B11_5.Click
        CreateClassBooking(B11_5.Text, B11_5.Tag)
    End Sub
    Private Sub B11_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B11_4.Click
        CreateClassBooking(B11_4.Text, B11_4.Tag)
    End Sub
    Private Sub B11_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B11_3.Click
        CreateClassBooking(B11_3.Text, B11_3.Tag)
    End Sub
    Private Sub B11_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B11_2.Click
        CreateClassBooking(B11_2.Text, B11_2.Tag)
    End Sub
    Private Sub B11_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B11_1.Click
        CreateClassBooking(B11_1.Text, B11_1.Tag)
    End Sub
    Private Sub B10_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B10_7.Click
        CreateClassBooking(B10_7.Text, B10_7.Tag)
    End Sub
    Private Sub B10_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B10_6.Click
        CreateClassBooking(B10_6.Text, B10_6.Tag)
    End Sub
    Private Sub B10_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B10_5.Click
        CreateClassBooking(B10_5.Text, B10_5.Tag)
    End Sub
    Private Sub B10_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B10_4.Click
        CreateClassBooking(B10_4.Text, B10_4.Tag)
    End Sub
    Private Sub B10_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B10_3.Click
        CreateClassBooking(B10_3.Text, B10_3.Tag)
    End Sub
    Private Sub B10_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B10_2.Click
        CreateClassBooking(B10_2.Text, B10_2.Tag)
    End Sub
    Private Sub B10_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B10_1.Click
        CreateClassBooking(B10_1.Text, B10_1.Tag)
    End Sub
    Private Sub B09_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B09_7.Click
        CreateClassBooking(B09_7.Text, B09_7.Tag)
    End Sub
    Private Sub B09_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B09_6.Click
        CreateClassBooking(B09_6.Text, B09_6.Tag)
    End Sub
    Private Sub B09_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B09_5.Click
        CreateClassBooking(B09_5.Text, B09_5.Tag)
    End Sub
    Private Sub B09_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B09_4.Click
        CreateClassBooking(B09_4.Text, B09_4.Tag)
    End Sub
    Private Sub B09_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B09_3.Click
        CreateClassBooking(B09_3.Text, B09_3.Tag)
    End Sub
    Private Sub B09_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B09_2.Click
        CreateClassBooking(B09_2.Text, B09_2.Tag)
    End Sub
    Private Sub B09_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B09_1.Click
        CreateClassBooking(B09_1.Text, B09_1.Tag)
    End Sub
    Private Sub B08_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B08_7.Click
        CreateClassBooking(B08_7.Text, B08_7.Tag)
    End Sub
    Private Sub B08_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B08_6.Click
        CreateClassBooking(B08_6.Text, B08_6.Tag)
    End Sub
    Private Sub B08_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B08_5.Click
        CreateClassBooking(B08_5.Text, B08_5.Tag)
    End Sub
    Private Sub B08_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B08_4.Click
        CreateClassBooking(B08_4.Text, B08_4.Tag)
    End Sub
    Private Sub B08_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B08_3.Click
        CreateClassBooking(B08_3.Text, B08_3.Tag)
    End Sub
    Private Sub B08_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B08_2.Click
        CreateClassBooking(B08_2.Text, B08_2.Tag)
    End Sub
    Private Sub B08_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B08_1.Click
        CreateClassBooking(B08_1.Text, B08_1.Tag)
    End Sub
    Private Sub B07_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B07_7.Click
        CreateClassBooking(B07_7.Text, B07_7.Tag)
    End Sub
    Private Sub B07_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B07_6.Click
        CreateClassBooking(B07_6.Text, B07_6.Tag)
    End Sub
    Private Sub B07_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B07_5.Click
        CreateClassBooking(B07_5.Text, B07_5.Tag)
    End Sub
    Private Sub B07_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B07_4.Click
        CreateClassBooking(B07_4.Text, B07_4.Tag)
    End Sub
    Private Sub B07_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B07_3.Click
        CreateClassBooking(B07_3.Text, B07_3.Tag)
    End Sub
    Private Sub B07_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B07_2.Click
        CreateClassBooking(B07_2.Text, B07_2.Tag)
    End Sub
    Private Sub B07_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B07_1.Click
        CreateClassBooking(B07_1.Text, B07_1.Tag)
    End Sub
    Private Sub B06_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B06_7.Click
        CreateClassBooking(B06_7.Text, B06_7.Tag)
    End Sub
    Private Sub B06_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B06_6.Click
        CreateClassBooking(B06_6.Text, B06_6.Tag)
    End Sub
    Private Sub B06_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B06_5.Click
        CreateClassBooking(B06_5.Text, B06_5.Tag)
    End Sub
    Private Sub B06_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B06_4.Click
        CreateClassBooking(B06_4.Text, B06_4.Tag)
    End Sub
    Private Sub B06_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B06_3.Click
        CreateClassBooking(B06_3.Text, B06_3.Tag)
    End Sub
    Private Sub B06_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B06_2.Click
        CreateClassBooking(B06_2.Text, B06_2.Tag)
    End Sub
    Private Sub B06_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B06_1.Click
        CreateClassBooking(B06_1.Text, B06_1.Tag)
    End Sub
    Private Sub B05_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B05_7.Click
        CreateClassBooking(B05_7.Text, B05_7.Tag)
    End Sub
    Private Sub B05_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B05_6.Click
        CreateClassBooking(B05_6.Text, B05_6.Tag)
    End Sub
    Private Sub B05_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B05_5.Click
        CreateClassBooking(B05_5.Text, B05_5.Tag)
    End Sub
    Private Sub B05_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B05_4.Click
        CreateClassBooking(B05_4.Text, B05_4.Tag)
    End Sub
    Private Sub B05_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B05_3.Click
        CreateClassBooking(B05_3.Text, B05_3.Tag)
    End Sub
    Private Sub B05_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B05_2.Click
        CreateClassBooking(B05_2.Text, B05_2.Tag)
    End Sub
    Private Sub B05_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B05_1.Click
        CreateClassBooking(B05_1.Text, B05_1.Tag)
    End Sub
    Private Sub B04_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B04_7.Click
        CreateClassBooking(B04_7.Text, B04_7.Tag)
    End Sub
    Private Sub B04_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B04_6.Click
        CreateClassBooking(B04_6.Text, B04_6.Tag)
    End Sub
    Private Sub B04_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B04_5.Click
        CreateClassBooking(B04_5.Text, B04_5.Tag)
    End Sub
    Private Sub B04_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B04_4.Click
        CreateClassBooking(B04_4.Text, B04_4.Tag)
    End Sub
    Private Sub B04_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B04_3.Click
        CreateClassBooking(B04_3.Text, B04_3.Tag)
    End Sub
    Private Sub B04_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B04_2.Click
        CreateClassBooking(B04_2.Text, B04_2.Tag)
    End Sub
    Private Sub B04_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B04_1.Click
        CreateClassBooking(B04_1.Text, B04_1.Tag)
    End Sub
    Private Sub B03_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B03_7.Click
        CreateClassBooking(B03_7.Text, B03_7.Tag)
    End Sub
    Private Sub B03_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B03_6.Click
        CreateClassBooking(B03_6.Text, B03_6.Tag)
    End Sub
    Private Sub B03_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B03_5.Click
        CreateClassBooking(B03_5.Text, B03_5.Tag)
    End Sub
    Private Sub B03_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B03_4.Click
        CreateClassBooking(B03_4.Text, B03_4.Tag)
    End Sub
    Private Sub B03_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B03_3.Click
        CreateClassBooking(B03_3.Text, B03_3.Tag)
    End Sub
    Private Sub B03_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B03_2.Click
        CreateClassBooking(B03_2.Text, B03_2.Tag)
    End Sub
    Private Sub B03_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B03_1.Click
        CreateClassBooking(B03_1.Text, B03_1.Tag)
    End Sub
    Private Sub B02_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B02_1.Click
        CreateClassBooking(B02_1.Text, B02_1.Tag)
    End Sub
    Private Sub B02_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B02_2.Click
        CreateClassBooking(B02_2.Text, B02_2.Tag)
    End Sub
    Private Sub B02_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B02_3.Click
        CreateClassBooking(B02_3.Text, B02_3.Tag)
    End Sub
    Private Sub B02_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B02_4.Click
        CreateClassBooking(B02_4.Text, B02_4.Tag)
    End Sub
    Private Sub B02_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B02_5.Click
        CreateClassBooking(B02_5.Text, B02_5.Tag)
    End Sub
    Private Sub B02_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B02_6.Click
        CreateClassBooking(B02_6.Text, B02_6.Tag)
    End Sub
    Private Sub B02_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B02_7.Click
        CreateClassBooking(B02_7.Text, B02_7.Tag)
    End Sub
    Private Sub B01_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B01_7.Click
        CreateClassBooking(B01_7.Text, B01_7.Tag)
    End Sub
    Private Sub B01_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B01_6.Click
        CreateClassBooking(B01_6.Text, B01_6.Tag)
    End Sub
    Private Sub B01_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B01_5.Click
        CreateClassBooking(B01_5.Text, B01_5.Tag)
    End Sub
    Private Sub B01_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B01_4.Click
        CreateClassBooking(B01_4.Text, B01_4.Tag)
    End Sub
    Private Sub B01_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B01_3.Click
        CreateClassBooking(B01_3.Text, B01_3.Tag)
    End Sub
    Private Sub B01_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B01_2.Click
        CreateClassBooking(B01_2.Text, B01_2.Tag)
    End Sub
    Private Sub B15_7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B15_7.Click
        CreateClassBooking(B15_7.Text, B15_7.Tag)
    End Sub


    Private Sub cmbStudio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStudio.SelectedIndexChanged
        'Try
        '    loadSCH()
        'Catch ex As Exception

        'End Try

    End Sub
    Private Sub cmbRoomType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRoomType.SelectedIndexChanged
        'Try
        '    loadSCH()
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub btnShow_Click(sender As System.Object, e As System.EventArgs) Handles btnShow.Click
        Try
            cekConection()
            loadSCH()
        Catch ex As Exception
            CustomeMessage("", "Error..", "Have some error while getting the svhedule..." & vbCrLf & vbCrLf & ex.Message)
            cekConection()
        End Try

    End Sub

    Private Sub frmClassSchedule_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub
End Class