﻿Public Class compdt
    Friend Shared cilintegertypes() As String = {"int8", "int16", "int32", "int64", "uint8", "uint16", "uint32", "uint64"}
    Friend Shared yointegertypes() As String = {"i8", "i16", "i32", "i64", "u8", "u16", "u32", "u64"}
    Friend Shared i8cmtypes() As String = {"int64", "uint64", "valuetype [mscorlib]System.Decimal"}
    Friend Shared expressionact() As String = {"+", "-", "/", "*"}
    Friend Shared expressionactopt() As String = {"add", "sub", "div", "mul"}
    Friend Shared errcap() As String = {"Error:", "error code="}
    Friend Shared argumentallow() As tokenhared.token = {tokenhared.token.TYPE_CO_STR, tokenhared.token.TYPE_DU_STR, tokenhared.token.TYPE_INT, tokenhared.token.TYPE_FLOAT, tokenhared.token.TYPE_BOOL, tokenhared.token.IDENTIFIER,
        tokenhared.token.EXPRESSION, tokenhared.token.NULL, tokenhared.token.TRUE, tokenhared.token.FALSE}
    Friend Shared blockopallow() As tokenhared.token = {tokenhared.token.TO, tokenhared.token.LOOP, tokenhared.token.TRY, tokenhared.token.CATCH}
    Friend Const FLAGPERFIX As String = "YO_Flag_"
    Friend Const YOMAINCLASS As String = "YO_Main"
    Friend Const DISPLAYILASMOUTPUT As Boolean = False
    Friend Shared DISPLAYTOKENWLEX As Boolean = False
    Friend Shared DISPLAYSTACKTRACE As Boolean = False
    Friend Const RUNCMDDELAY As Integer = 500
    Friend Const WAITILCODE As String = "call string [mscorlib]System.Console::ReadLine()
pop"
    Friend Const PARAM_IMPASSETS As String = "--import_assets"
    Friend Const PARAM_DEBUG As String = "--debug"
    Friend Const PARAM_DEBUG_IMPL As String = "--debug_impl"
    Friend Const PARAM_DEBUG_OPT As String = "--debug_opt"
    Friend Const PARAM_DISPLAYTOKENLEX As String = "--display_token"
    Friend Const PARAM_DISPLAYSTACKTRACE As String = "--stack_trace"
End Class
