using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Reflection;

public static class Helpers
{

    //maybe use inline annoymus classes? :D

    //Almsot there, but need to get the second
    //Better way using reflection, but still have to pass MethodBase :/
    /*
    public static string OutputManyParams(MethodBase method, params object[] args)
    {
        string toRet = "";
        for (var i = 0; i < args.Length; i++)
        {
            string varname = method.GetParameters()[1].[i].Name;
            toRet += varname + ": " + args[i].ToString() + ",";
        }
        toRet = toRet.Substring(0, toRet.Length - 1); //To remove the last ,
        return toRet;
    }

    public static void DebugOutputManyParams(MethodBase method, params object[] args)
    {
        Debug.Log(OutputManyParams(method, args));
    }

    */





/*
    /////// FAILED EXPIREMENT USING LINQ EXPRESSIONS TO GET VAR NAMES
    //Helper function to output a bunch of variables at the same time without having to hardcode it. Advanced technique using boxing/upcasting/reflection/expressions
    public static string FAILED_OutputManyParams(params object[] toOutArr)
    {
        string toRet = "";
        for(int i = 0; i < toOutArr.Length; i++)
        {
            toRet += GetMemberName(() => toOutArr[0]) + ": " + toOutArr[0].ToString() + ",";
        }
        toRet = toRet.Substring(0, toRet.Length - 1); //To remove the last ,
        return toRet;
    }


    //Reflection & Expressions, used to get the name of a varaible
    //https://stackoverflow.com/questions/716399/how-do-you-get-a-variables-name-as-it-was-physically-typed-in-its-declaration
    public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }*/
}
