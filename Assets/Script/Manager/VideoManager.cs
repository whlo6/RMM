using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class VideoManager : Mono<VideoManager>
{
    [Header("播放倍速")]
    public float playSpeed = 1f; // 默认1倍速，2=两倍速，0.5=半速
    [Header("视频显示UI")]
    public RawImage videoDisplay;
    [Header("全局音量 0~1")]
    [Range(0, 1)] public float videoVolume = 1f;

    public GameObject Speed;
    public GameObject Skip;

    private VideoPlayer _videoPlayer;
    private AudioSource _audioSource;
    private RenderTexture _rt;
    private bool _isVideoReady = false;

    private void Awake()
    {
        DontDestroyThis();
        InitPlayer();
    }

    private void InitPlayer()
    {
        if (_videoPlayer == null)
            _videoPlayer = gameObject.AddComponent<VideoPlayer>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        // 基础设置
        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        _videoPlayer.playOnAwake = false;
        _audioSource.playOnAwake = false;
        _audioSource.volume = videoVolume;
        _audioSource.mute = false;

        // ========== 音频核心修复代码（之前缺失）==========
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        // 指定需要控制的音轨数量
        _videoPlayer.controlledAudioTrackCount = 1;
        // 绑定AudioSource到0号音轨
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        // 强制启用0号音轨（默认是关闭状态！无声核心元凶）
        _videoPlayer.EnableAudioTrack(0, true);
        // ===============================================

        // 监听加载完成、播放完成事件
        _videoPlayer.prepareCompleted += OnVideoPrepareFinish;
        _videoPlayer.loopPointReached += OnVideoPlayFinish;
    }

    /// <summary>
    /// 播放视频，修复音轨无声问题，异步加载等待就绪
    /// </summary>
    /// <param name="path">Resources路径不带后缀 / StreamingAssets完整路径</param>
    /// <param name="loop">是否循环</param>
    /// <param name="mute">是否静音播放</param>
    public void PlayVideo(string path, bool loop = false, bool mute = false)
    {
        if (videoDisplay == null || string.IsNullOrEmpty(path))
        {
            Debug.LogError("RawImage未赋值或视频路径为空");
            return;
        }

        StopVideo();
        _isVideoReady = false;

        // 创建渲染纹理
        _rt = new RenderTexture(1920, 1080, 24);
        _rt.Create();
        _videoPlayer.targetTexture = _rt;
        videoDisplay.texture = _rt;

        // 加载视频资源
        if (File.Exists(path))
        {
            // StreamingAssets本地文件
            _videoPlayer.source = VideoSource.Url;
            _videoPlayer.url = path;
        }
        else
        {
            // Resources文件夹资源（路径不带.mp4）
            VideoClip clip = Resources.Load<VideoClip>(path);
            if (clip == null)
            {
                Debug.LogError($"找不到视频资源: {path}");
                return;
            }
            _videoPlayer.source = VideoSource.VideoClip;
            _videoPlayer.clip = clip;
        }

        _videoPlayer.isLooping = loop;
        _audioSource.mute = mute;
        videoDisplay.gameObject.SetActive(true);

        // 设置倍速
        _videoPlayer.playbackSpeed = playSpeed;
        // 关键：先预加载视频，解析音轨，就绪后自动播放
        _videoPlayer.Prepare();
    }

    // 视频预加载完成（音轨此时才会被完整读取）
    private void OnVideoPrepareFinish(VideoPlayer player)
    {
        _isVideoReady = true;
        Debug.Log($"视频加载完成，音轨数量：{player.audioTrackCount}");
        // 同步当前倍速
        player.playbackSpeed = playSpeed;
        // 再次强制激活音轨，防止预加载后失效
        player.EnableAudioTrack(0, true);
        player.Play();
    }

    // 播放完毕自动关闭
    private void OnVideoPlayFinish(VideoPlayer player)
    {
        if (player.isLooping) return;
        StopVideo();
    }

    /// <summary>
    /// 设置视频音量
    /// </summary>
    public void SetVolume(float vol)
    {
        videoVolume = Mathf.Clamp01(vol);
        if (_audioSource != null)
            _audioSource.volume = videoVolume;
    }

    /// <summary>
    /// 切换静音
    /// </summary>
    public void SetMute(bool isMute)
    {
        if (_audioSource != null)
            _audioSource.mute = isMute;
    }

    // 停止视频（全判空杜绝MissingReference报错）
    public void StopVideo()
    {
        if (_videoPlayer != null)
        {
            _videoPlayer.Stop();
            _videoPlayer.clip = null;
            _videoPlayer.url = "";
        }

        if (_audioSource != null)
        {
            _audioSource.Stop();
        }

        if (videoDisplay != null)
        {
            videoDisplay.texture = null;
            videoDisplay.gameObject.SetActive(false);
        }

        if (_rt != null)
        {
            Destroy(_rt);
            _rt = null;
        }
    }

    public void PauseVideo()
    {
        if (_isVideoReady)
            _videoPlayer?.Pause();
    }

    private void OnDestroy()
    {
        // 销毁解绑事件，防止内存泄漏
        if (_videoPlayer != null)
        {
            _videoPlayer.prepareCompleted -= OnVideoPrepareFinish;
            _videoPlayer.loopPointReached -= OnVideoPlayFinish;
            _videoPlayer.Stop();
        }
        if (_audioSource != null)
            _audioSource.Stop();
        if (_rt != null)
            Destroy(_rt);
    }

    /// <summary>
    /// 设置视频播放速度
    /// </summary>
    /// <param name="speed">播放速度，建议0~4区间</param>
    public void SetPlaySpeed(float speed)
    {
        if (playSpeed != 1f)
        {
            Debug.Log("恢复播放速度");
            speed = 1f;//二次调用则恢复速度
            this.Speed.SetActive(false);
        }
        else
        {
            Debug.Log("开启两倍速播放");
            this.Speed.SetActive(true);
        }
            
        // 限制合理区间，防止速度异常
        playSpeed = Mathf.Clamp(speed, 0.1f, 4f);
        if (_videoPlayer != null)
        {
            _videoPlayer.playbackSpeed = playSpeed;
        }
    }

    public void SetPlaySkip()
    {
        PlayerManager.Instance.isPlayCartoon = !PlayerManager.Instance.isPlayCartoon;
        this.Skip.SetActive(!this.Skip.active);
    }
}