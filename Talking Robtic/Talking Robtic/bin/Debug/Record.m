function [y] = Record()
%UNTITLED �˴���ʾ�йش˺�����ժҪ
%   �˴���ʾ��ϸ˵��
fs=16000;           %ȡ��Ƶ��
duration=1;         %¼��ʱ��
record = audiorecorder(duration*fs,8,1);
recordblocking(record,5);
y = getaudiodata(record);
end

