#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
#endregion

namespace MHacksTestOne
{
    class Music
    {
        int stream;
        int streamFX;
        bool playing;
        int fxFlanger;
        bool flanger;
        int fxReverb;
        bool reverb;
        int fxLowFilter;
        int fxHighFilter;
        bool filter;
        int fxChorus;
        bool chorus;
        int fxGargle;
        bool gargle;
        int fxDistortion;
        bool distortion;

        public Music(String filename)
        {
            // initialize bools
            playing = false;
            flanger = false;
            reverb = false;
            filter = false;
            chorus = false;
            gargle = false;
            distortion = false;

            // init BASS using the default output device 
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                // create a stream channel from a file
                stream = Bass.BASS_StreamCreateFile("Content/cinema.wav", 0, 0, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FX);
                streamFX = BassFx.BASS_FX_TempoCreate(stream, BASSFlag.BASS_FX_FREESOURCE);
                if (stream == 0)
                {
                    // error creating the stream 
                    Console.WriteLine("Stream error: {0}", Bass.BASS_ErrorGetCode());
                }
                
            }
        }

        public void playPause()
        {
            if (playing)
            {
                Bass.BASS_ChannelPause(streamFX);
            }
            else
            {
                Bass.BASS_ChannelPlay(streamFX, false);
            }

            playing = !playing;
        }

        public void stop()
        {
            Bass.BASS_ChannelStop(streamFX);
            playing = false;
        }

        public void unload()
        {
            // free the stream 
            Bass.BASS_StreamFree(streamFX);
            // free BASS 
            Bass.BASS_Free();
        }

        public void toggleFilter()
        {
            if (!filter)
            {
                BASS_BFX_BQF highFilter = new BASS_BFX_BQF();
                fxHighFilter = Bass.BASS_ChannelSetFX(streamFX, BASSFXType.BASS_FX_BFX_BQF, 1);
                highFilter.lFilter = BASSBFXBQF.BASS_BFX_BQF_HIGHPASS;
                highFilter.fCenter = 300f;
                Bass.BASS_FXSetParameters(fxHighFilter, highFilter);
                BASS_BFX_BQF lowFilter = new BASS_BFX_BQF();
                fxLowFilter = Bass.BASS_ChannelSetFX(streamFX, BASSFXType.BASS_FX_BFX_BQF, 1);
                lowFilter.lFilter = BASSBFXBQF.BASS_BFX_BQF_LOWPASS;
                lowFilter.fCenter = 3000f;
                Bass.BASS_FXSetParameters(fxLowFilter, lowFilter);
            }
            else
            {
                Bass.BASS_ChannelRemoveFX(streamFX, fxLowFilter);
                Bass.BASS_ChannelRemoveFX(streamFX, fxHighFilter);
            }

            filter = !filter;
        }

        // This creates a bandpass filter which allows the given range of frequencies.  When using this to create
        // fade effects, stick to changing the low by 1 for every 43.333 you change the high (i.e. 0-16000 is
        // "no filter", 250-5166.675, 300-3000 and so on)
        public void transformFilter(float low, float high)
        {
            if (!filter)
            {
                BASS_BFX_BQF highFilter = new BASS_BFX_BQF();
                fxHighFilter = Bass.BASS_ChannelSetFX(streamFX, BASSFXType.BASS_FX_BFX_BQF, 1);
                highFilter.lFilter = BASSBFXBQF.BASS_BFX_BQF_HIGHPASS;
                highFilter.fCenter = low;
                Bass.BASS_FXSetParameters(fxHighFilter, highFilter);
                BASS_BFX_BQF lowFilter = new BASS_BFX_BQF();
                fxLowFilter = Bass.BASS_ChannelSetFX(streamFX, BASSFXType.BASS_FX_BFX_BQF, 1);
                lowFilter.lFilter = BASSBFXBQF.BASS_BFX_BQF_LOWPASS;
                lowFilter.fCenter = high;
                Bass.BASS_FXSetParameters(fxLowFilter, lowFilter);
            }
            else
            {
                BASS_BFX_BQF highFilter = new BASS_BFX_BQF();
                highFilter.lFilter = BASSBFXBQF.BASS_BFX_BQF_HIGHPASS;
                highFilter.fCenter = low;
                Bass.BASS_FXSetParameters(fxHighFilter, highFilter);
                BASS_BFX_BQF lowFilter = new BASS_BFX_BQF();
                lowFilter.lFilter = BASSBFXBQF.BASS_BFX_BQF_LOWPASS;
                lowFilter.fCenter = high;
                Bass.BASS_FXSetParameters(fxLowFilter, lowFilter);
            }
            filter = true;
        }

        public bool isFiltered()
        {
            return filter;
        }

        public void toggleChorus()
        {
            if (!chorus)
            {
                fxChorus = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_CHORUS, 1);
            }
            else
            {
                Bass.BASS_ChannelRemoveFX(stream, fxChorus);
            }

            chorus = !chorus;
        }

        public void toggleFlanger()
        {
            if (!flanger)
            {
                fxFlanger = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_FLANGER, 1);
            }
            else
            {
                Bass.BASS_ChannelRemoveFX(stream, fxFlanger);
            }

            flanger = !flanger;
        }

        public void toggleReverb()
        {
            if (!reverb)
            {
                fxReverb = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_REVERB, 1);
            }
            else
            {
                Bass.BASS_ChannelRemoveFX(stream, fxReverb);
            }

            reverb = !reverb;
        }

        public void toggleGargle()
        {
            if (!gargle)
            {
                fxGargle = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_GARGLE, 1);
            }
            else
            {
                Bass.BASS_ChannelRemoveFX(stream, fxGargle);
            }

            gargle = !gargle;
        }

        public void toggleDistortion()
        {
            if (!distortion)
            {
                fxDistortion = Bass.BASS_ChannelSetFX(stream, BASSFXType.BASS_FX_DX8_DISTORTION, 1);
            }
            else
            {
                Bass.BASS_ChannelRemoveFX(stream, fxDistortion);
            }

            distortion = !distortion;
        }

        // 10 is a reasonable value for editing speed.  Higher the number, bigger the change.
        public void speedUp(float amount)
        {
            float speed = 0.0f;
            Bass.BASS_ChannelGetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO, ref speed);
            Bass.BASS_ChannelSetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO, speed + amount);
        }

        public void slowDown(int amount)
        {
            float speed = 0.0f;
            Bass.BASS_ChannelGetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO, ref speed);
            Bass.BASS_ChannelSetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO, speed - amount); 
        }

        // 1 semitone is one half-step.  I would stick to that unless you need something more.
        public void pitchUp(int semitones)
        {
            float pitch = 0.0f;
            Bass.BASS_ChannelGetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, ref pitch);
            Bass.BASS_ChannelSetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, pitch + semitones);
        }

        public void pitchDown(int semitones)
        {
            float pitch = 0.0f;
            Bass.BASS_ChannelGetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, ref pitch);
            Bass.BASS_ChannelSetAttribute(streamFX, BASSAttribute.BASS_ATTRIB_TEMPO_PITCH, pitch - semitones);
        }

    }
}
