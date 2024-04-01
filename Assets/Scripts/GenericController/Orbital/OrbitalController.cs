using System.Collections;
using UnityEngine;

namespace WI
{
    public class OrbitalController : GenericController
    {
        public new OrbitalControllerOption option => base.option as OrbitalControllerOption;

        public override void Movement()
        {
            nextPosition = option.target.position;

            Zoom();
            Rotate();
            Move();
        }

        protected override void Move()
        {
            if (option.isKoreanPeninsula)
            {
                KoreanPeninsulaMove();
            }
            else
            {
                GlobeMove();
            }
        }
        private void KoreanPeninsulaMove()
        {
            if (!input.leftClick || option.moveLimit)
            {
                return;
            }
            moveVector = transform.TransformDirection(input.mouseX, input.mouseY, 0) * option.moveSpeed * option.moveSensivity;
            nextPosition = option.target.position - moveVector;
        }
        private void GlobeMove()
        {
            if(!input.leftClick || option.moveLimit)
            {
                return;
            }
            AzimuthControl();
            ElevationControl();
        }

        protected override void Zoom()
        {
            if (input.mouseWheel < -0.01f || input.mouseWheel > 0.01f)
            {
                var nextDistance = option.currentDistance - input.mouseWheel * option.zoomSpeed;
                option.currentDistance = Mathf.Lerp(option.currentDistance, nextDistance, option.zoomSpeed * Time.deltaTime);
                option.currentDistance = Mathf.Clamp(option.currentDistance, option.minDistance, option.maxDistance);
            }
        }

        protected override void Rotate()
        {
            if (option.isKoreanPeninsula)
            {
                if (!input.wheelClick)
                    return;

                AzimuthControl();
                ElevationControl();
            }
        }
        protected void AzimuthControl()
        {
            if (option.azimuthRotateLimit )
                return;
            if (input.mouseX > 0 || input.mouseX < 0)
            {
                option.currentAzimuth += input.mouseX * option.azimuthSensivity;
                if (option.currentAzimuth > 360)
                    option.currentAzimuth -= 360;
                else if (option.currentAzimuth < 0)
                    option.currentAzimuth += 360;
            }
        }

        protected void ElevationControl()
        {
            if (option.elevationRotateLimit || option.orthographic)
                return;
            if (input.mouseY > 0.01f || input.mouseY < -0.01f)
            {
                option.currentElevation -= input.mouseY * option.elevationSensivity;
                option.currentElevation = Mathf.Clamp(option.currentElevation, option.minElevation, option.maxElevation);
            }
        }

        public override void LastPositioning(bool limit)
        {
            if (!limit)
                return;

            option.target.position = nextPosition;
            var dist = new Vector3(0, 0, -option.currentDistance);

            cameraPosition = nextPosition + Quaternion.Euler(option.currentElevation, option.currentAzimuth, 0f) * dist;
            camera.transform.position = cameraPosition;

            if (option.currentElevation <= 90f)
            {
                camera.transform.rotation = Quaternion.Euler(option.currentElevation, option.currentAzimuth, 0f);
            }
            else
            {
                camera.transform.LookAt(option.target);
            }
        }

        public override void Rewind()
        {
            option.target.position = option.originTargetPos;
            option.target.eulerAngles = option.originTargetRot;
            option.currentDistance = option.originDistance;
            option.currentElevation = option.originElevation;
            option.currentAzimuth = option.originAzimuth;
            var dist = new Vector3(0, 0, -option.currentDistance);
            cameraPosition = option.target.position + Quaternion.Euler(option.currentElevation, option.currentAzimuth, 0f) * dist;
            camera.transform.position = cameraPosition;
            camera.transform.LookAt(option.target);
        }
    }
}