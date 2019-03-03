function [ ] = Record_Save( y,path )
%UNTITLED 此处显示有关此函数的摘要
%   此处显示详细说明
audiowrite(path,y,16000);
end

