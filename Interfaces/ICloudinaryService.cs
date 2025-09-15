using System;

namespace P1WebMVC.Interfaces;

public interface ICloudinaryService
{

    public string UploadImage(IFormFile image);

    public string UploadImageAsync(IFormFile image);


    public string UploadVideo(IFormFile video);


    public string UploadVideoAsync(IFormFile video);



}
