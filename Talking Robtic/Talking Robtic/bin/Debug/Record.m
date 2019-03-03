function [y] = Record()
%UNTITLED 此处显示有关此函数的摘要
%   此处显示详细说明
fs=16000;           %取样频率
duration=1;         %录音时间
record = audiorecorder(duration*fs,8,1);
recordblocking(record,5);
y = getaudiodata(record);
end

