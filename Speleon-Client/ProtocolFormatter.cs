﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speleon_Client
{
    static class ProtocolFormatter
    {
        public enum CMDType
        {
            SignIn,
            SignInSuccessfully,
            SignInUnsuccessfully,
            Message
        }

        /// <summary>
        /// 获取指定协议类型的正则表达式
        /// </summary>
        /// <param name="cmdType">协议类型</param>
        /// <returns>指定协议的正则表达式</returns>
        static public string GetProtocolPattern(CMDType cmdType)
        {
            UnityModule.DebugPrint("开始获取协议正则表达式：{0}", cmdType.ToString());
            //每条协议最后使用换行符结束
            switch (cmdType)
            {
                case CMDType.Message:
                    {
                        return "";// "HEY_CVER=(?<clientversion>.+?)_CMDTYPE=(?<cmdtype>.+?)_USERID=(?<userid>.+?)_PASSWORD=(?<password>.+?)\n";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        /// <summary>
        /// 获取按指定协议类型格式化的协议字符串
        /// </summary>
        /// <param name="cmdType">协议类型</param>
        /// <returns>协议字符串</returns>
        static public string FormatProtocol(CMDType cmdType,params object[] ProtocalValues)
        {
            UnityModule.DebugPrint("开始格式化通信协议：{0}-{1}",cmdType.ToString(),string.Join("/",ProtocalValues));
            //每条协议最后加一个换行符，否则服务端无法使用正则匹配最后一个参数
            try
            {
                switch (cmdType)
                {
                    case CMDType.SignIn:
                        {
                                return string.Format("HEY_CVER={0}_CMDTYPE=SIGNIN_USERID={1}_PASSWORD={2}\n", ProtocalValues[0], ProtocalValues[1], ProtocalValues[2]);
                        }
                    case CMDType.SignInSuccessfully:
                        {
                            return "HI_CMDTYPE=SIGNINSUCCESSFULLY";
                        }
                    case CMDType.SignInUnsuccessfully:
                        {
                            return "HI_CMDTYPE=SIGNINUNSUCCESSFULLY";
                        }
                    default:
                        {
                            return "";
                        }
                }
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("获取 {0} 通信协议时遇到错误：{1}",cmdType.ToString(),ex.Message);
                return "";
            }
        }

    }
}
