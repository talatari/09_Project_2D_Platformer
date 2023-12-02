public class Level
{
    private PlayerFactory _playerFactory;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private PlayerCollisionDetector _playerColliosionDetector;

    public Level(PlayerFactory playerFactory, PlayerModel playerModel)
    {
        _playerFactory = playerFactory;
        _playerModel = playerModel;
    }

    public void Start()
    {
        _playerModel = _playerFactory.CreatePlayerModel();
        _playerView = _playerFactory.CreatePlayerView();
        _playerView.Init(_playerModel);
        _playerColliosionDetector = _playerFactory.CreateCollisionDetector();
        _playerColliosionDetector.Init(_playerModel);
    }
}