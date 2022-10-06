using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrul : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    [SerializeField] private float _waitingTime;

    //массив содержащий время до каждой точки
    private float _currentTime;
    private float _waitingTimer;
    private int _startIndex; // для подсчета индекса массива _points , где i - стартовая точка
    private int _endIndex = 1; // для подсчета индекса массива _points , где m - точка назначения точка
    private bool _isLastPoint = false; //счетчик (движение по точкам начинается заново)


    // Update is called once per frame


    void Update()
    {
        _waitingTimer += Time.deltaTime; // таймер ожидания
        if (_waitingTimer <= _waitingTime)
        {
            return;
        }

        var pointsSize = _points.Length;
        if (_endIndex == pointsSize) // для того , чтобы начать движение заноево
        {
            _startIndex = pointsSize - 1;
            _endIndex = 0;
            _isLastPoint = true;
        }

        var distance = Vector3.Distance(_points[_startIndex].position, _points[_endIndex].position);
        var travelTime = distance / _speed;


        _currentTime += Time.deltaTime;
        var progress = _currentTime / travelTime;

        var result = Vector3.Lerp(_points[_startIndex].position, _points[_endIndex].position, progress);
        transform.position = result;


        if (_currentTime > travelTime)
        {
            _waitingTimer = 0;

            _currentTime = 0;
            _startIndex++; // увеличиваем нащи индексы
            _endIndex++;
            if (_isLastPoint)
            {
                _startIndex = 0;
                _endIndex = 1;
                _isLastPoint = false;
            }
        }
    }
}