using Fall2020_CSC403_Project.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace MyGameLibrary
{
    public class MusicPlayer
    {

        private static BlockAlignReductionStream gameOverSound;
        private static BlockAlignReductionStream levelSound;
        private static BlockAlignReductionStream titleSound;
        private static BlockAlignReductionStream battleSound;
        private static BlockAlignReductionStream bossBattleSound;

        private static WaveOut waveOut;

        private static LoopedStream loopedtitleSound;
        private static LoopedStream loopedbattleSound;
        private static LoopedStream loopedlevelSound;
        private static LoopedStream loopedgameOverSound;
        private static LoopedStream loopedbossBattleSound;

        public static void InitializeSounds()
        {
            waveOut = new WaveOut();

            levelSound = new BlockAlignReductionStream(new AudioFileReader("../../Resources/669543__sintelv__jungle-level.wav"));
            gameOverSound = new BlockAlignReductionStream(new AudioFileReader("../../Resources/365738__matrixxx__game-over-02.wav"));
            titleSound = new BlockAlignReductionStream(new AudioFileReader("../../Resources/460432__jay_you__music-elevator.wav"));
            battleSound = new BlockAlignReductionStream(new AudioFileReader("../../Resources/338817__sirkoto51__rpg-battle-loop-1.wav"));
            bossBattleSound = new BlockAlignReductionStream(new AudioFileReader("../../Resources/352171__sirkoto51__boss-battle-loop-1.wav"));

            loopedlevelSound = new LoopedStream(levelSound);
            loopedgameOverSound = new LoopedStream(gameOverSound);
            loopedtitleSound = new LoopedStream(titleSound);
            loopedbattleSound = new LoopedStream(battleSound);
            loopedbossBattleSound = new LoopedStream(bossBattleSound);
            }

        public static void PlayLevelMusic()
        {
            waveOut.Init(loopedlevelSound);
            waveOut.Play();
        }

        public static void ChangeLevelVolume(float volume)
        {
            var volumeProvider = new WaveChannel32(loopedlevelSound);
            volumeProvider.Volume = volume;
            waveOut.Init(volumeProvider);
            waveOut.Play();
        }

        public static void StopLevelMusic()
        {
            waveOut.Stop();
        }

        public static void PlayTitleSound()
        {
            waveOut.Init(loopedtitleSound);
            waveOut.Play();
        }

        public static void StopTitleSound()
        {
            waveOut.Stop();
        }

        public static void PlayGameOverSound()
        {
            waveOut.Init(loopedgameOverSound);
            waveOut.Play();
        }

        public static void PlayBattleSound()
        {
            waveOut.Init(loopedbattleSound);
            waveOut.Play();
        }

        public static void StopBattleSound()
        {
            waveOut.Stop();
        }

    }

    
}
