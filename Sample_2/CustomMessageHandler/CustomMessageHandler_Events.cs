using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
namespace Sample_2
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        /// <summary>
        /// 微信客户端（通过微信服务器）自动发送过来的位置信息事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        protected override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这里写什么都无所谓，比如：上帝爱你！";
            return responseMessage;//这里也可以返回null（需要注意写日志时候null的问题）
        }
        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        protected override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = @"您可以发送【文字】【位置】【图片】【语音】等不同类型的信息，查看不同格式的回复。
                     您也可以直接点击菜单查看各种类型的回复。";
            return responseMessage;
        }
        /// <summary>
        /// 退订
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        /// <returns></returns>
        protected override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "有空再来";
            return responseMessage;
        }
        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        /// <returns></returns>
        protected override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            //菜单点击，需要跟创建菜单时的Key匹配
            switch (requestMessage.EventKey)
            {
                case "OneClick":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您点击了底部按钮。";
                        reponseMessage = strongResponseMessage;
                    }
                    break;
                case "SubClickRoot_Text":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您点击了子菜单按钮。";
                        reponseMessage = strongResponseMessage;
                    }
                    break;
            }
            return reponseMessage;
        }
        /// <summary>
        /// 扫描带参数二维码事件
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// scan事件的意义在于获取扫描二维码中包含的参数，便于识别和统计用户扫描了哪个二维码。
        /// </summary>
        /// <returns></returns>
        protected override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "感谢扫码";
            return responseMessage;
        }
    }
}