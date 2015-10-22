using Microsoft.VisualStudio.TestTools.UnitTesting;
using DingTalk.SDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingTalk.SDK.Utilitys;

namespace DingTalk.SDK.Entities.Tests
{
    [TestClass()]
    public class DDOAMessageTests
    {
        [TestMethod()]
        public void DDOAMessageTest()
        {
            string json = @"{
                'msgtype': 'oa',
     'oa': {
                    'message_url': 'http://dingtalk.com',
        'head': {
                        'bgcolor': 'FFBBBBBB',
            'text': '头部标题'
        },
        'body': {
                        'title': '正文标题',
            'form': [
                {
                    'key': '姓名:',
                    'value': '张三'
                },
                {
                    'key': '年龄:',
                    'value': '20'
                },
                {
                    'key': '身高:',
                    'value': '1.8米'
                },
                {
                    'key': '体重:',
                    'value': '130斤'
                },
                {
                    'key': '学历:',
                    'value': '本科'
                },
                {
                    'key': '爱好:',
                    'value': '打球、听音乐'
                }
            ],
            'rich': {
                'num': '15.6',
                'unit': '元'
            },
            'content': '大段文本大段文本大段文本大段文本大段文本大段文本大段文本大段文本大段文本大段文本大段文本大段文本',
            'image': '@lADOADmaWMzazQKA',
            'file_count': '3',
            'author': '李四 '
        }
        }
    }";

            var msg = JsonUtility.Deserialize<DDOAMessage>(json);

            Assert.IsNotNull(msg);
            Assert.IsTrue(msg.oa.body.form.Length > 0);
            Assert.IsNotNull(msg.oa.body.image);
            Assert.IsNotNull(msg.oa.body.file_count);
            Assert.IsNotNull(msg.oa.body.author);
            Assert.AreEqual(msg.oa.body.form[0].Key, "姓名:");
        }
    }
}