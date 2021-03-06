﻿//
// Copyright (C) 2019 Allow2
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
// the License.
//
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and limitations under the License.
//
namespace Allow2.Allow2Examples
{

    using UnityEngine;
    using UnityEngine.UI;

    public class SceneClass : MonoBehaviour
    {

        public RawImage qrImage;

        void Awake()
        {
            // staging is only really for Allow2 internal development, omit this line in your code
            Allow2.env = EnvType.Staging;

            // set the deviceToken
            // this needs top be done before ANY calls to the Allow2 platform
            // create your own deviceToken at https://developer.allow2.com for free
            // use it to manage your app/game/device and track metrics
            // it's also important to use your own deviceToken and app definition in order to make use of
            // the additional marketing channel opportunities that Allow2 provides for free
            Allow2.DeviceToken = "B0hNax6VCFi9vphu";

            // We can also now check if the device/app/game is already paired and what children are in the account
            Debug.Log("isPaired: " + Allow2.IsPaired);
            Debug.Log("Children: " + Allow2.Children);

            // Not Required if the textfield sets the name on display, it will also trigger an update of the QR Code
            // and in the pairing interface, we need a QR code to make the process simple for our users
            //Allow2.GetQR(this, SystemInfo.deviceName, delegate (string err, Texture2D qrCode)
            //{
            //    Debug.Log("qrcode error: " + (err ?? "No Error") + " : " + (qrCode ? "yes" : "no"));
            //    Debug.Log(qrImage.GetComponent<RawImage>());
            //    qrImage.GetComponent<RawImage>().texture = qrCode;
            //});

            // usually start the pairing background process here,
            // unless you have opted to not allow pairing with QR Code (but this is highly recommended for a better user experience)
            Allow2.StartPairing(this, delegate (string err, Allow2CheckResult result) {
                // this may be called several times with errors
                if (err != null) {
                    Debug.Log(err);
                    return;
                }
                // once paired, the pairing process will automatically stop itself
                // you should close off the pairing interface here and display success to the end user
                Debug.Log("isPaired: " + Allow2.IsPaired);
                Debug.Log("Children: " + Allow2.Children);
            });
        }
    }
}
