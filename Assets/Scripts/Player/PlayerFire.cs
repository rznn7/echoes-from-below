using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour, IPopupMessageSender
{
    public event Action<List<PopupMessage>, MessagePriority> AddMessagesToQueue;

    (float x, float y) _aim = (0f, 0f);

    List<PopupMessage> _popupFireReport;

    public void Fire()
    {
        var currentBulletCount = GameUIManager.instance.bullets.value;

        _popupFireReport = new List<PopupMessage>();

        if (currentBulletCount > 0)
        {
            var targetCell = GetTargetCell();

            _popupFireReport.Add(
                new PopupMessage($"Torpedo fired at coordinates {{x:{targetCell.x};y:{targetCell.y}}}", "OK"));

            CheckForHit(targetCell);

            GameUIManager.instance.bullets.updateValue(currentBulletCount - 1);
        }
        else
        {
            _popupFireReport.Add(new PopupMessage("Firing impossible. No torpedo", "OK"));
        }

        AddMessagesToQueue?.Invoke(_popupFireReport, MessagePriority.High);
    }

    public void UpdateFireHorizontalAim(float newValue)
    {
        _aim.x = newValue;
    }

    public void UpdateFireVerticalAim(float newValue)
    {
        _aim.y = newValue;
    }

    (float x, float y) GetTargetCell()
    {
        return (_aim.x + transform.position.x, _aim.y + transform.position.z);
    }

    void CheckForHit((float x, float y) targetCell)
    {
        var targets = FindObjectsOfType<TorpedoHittable>();

        foreach (var target in targets)
        {
            if (!Mathf.Approximately(target.transform.position.x, targetCell.x) ||
                !Mathf.Approximately(target.transform.position.z, targetCell.y))
                continue;

            Destroy(target.gameObject);

            _popupFireReport.Add(
                new PopupMessage($"Torpedo hit reported at coordinates {{x:{targetCell.x};y:{targetCell.y}}}",
                    "OK"));

            return;
        }
    }
}
