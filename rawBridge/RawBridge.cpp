#include "pch.h"
#include "libraw/libraw.h"
#include <cstring>
#include <cstdlib>

extern "C" __declspec(dllexport)
int LoadRawImage(
    const char* path,
    unsigned short** outBuffer,   // 🔥 ushort now
    int* width,
    int* height,
    int* bitsPerChannel           // 🔥 tell VB what we output
)
{
    LibRaw raw;

    if (raw.open_file(path) != LIBRAW_SUCCESS)
        return -1;

    if (raw.unpack() != LIBRAW_SUCCESS)
        return -2;

    // 🔥 FORCE 16-bit processing
    raw.imgdata.params.output_bps = 16;
    raw.imgdata.params.no_auto_bright = 1;  // optional but recommended

    if (raw.dcraw_process() != LIBRAW_SUCCESS)
        return -3;

    libraw_processed_image_t* img =
        raw.dcraw_make_mem_image();

    if (!img)
        return -4;

    *width = img->width;
    *height = img->height;
    *bitsPerChannel = img->bits;  // should now be 16

    int pixelCount = img->width * img->height * img->colors;
    int dataSize = pixelCount * sizeof(unsigned short);

    *outBuffer = (unsigned short*)malloc(dataSize);
    memcpy(*outBuffer, img->data, dataSize);

    LibRaw::dcraw_clear_mem(img);
    raw.recycle();

    return 0;
}

extern "C" __declspec(dllexport)
int LoadRawPreview(
    const char* path,
    unsigned char** outBuffer,
    int* dataSize,
    int* width,
    int* height,
    int* imageType
)
{
    LibRaw raw;

    if (raw.open_file(path) != LIBRAW_SUCCESS)
        return -1;

    if (raw.unpack_thumb() != LIBRAW_SUCCESS)
        return -2;

    libraw_processed_image_t* thumb =
        raw.dcraw_make_mem_thumb();

    if (!thumb)
        return -3;

    *dataSize = thumb->data_size;
    *width = thumb->width;
    *height = thumb->height;
    *imageType = thumb->type;

    *outBuffer = (unsigned char*)malloc(*dataSize);
    memcpy(*outBuffer, thumb->data, *dataSize);

    LibRaw::dcraw_clear_mem(thumb);
    raw.recycle();

    return 0;
}




extern "C" __declspec(dllexport)
void FreeRawBuffer(unsigned char* buffer)
{
    free(buffer);
}