public class Level
{
    private PlayerFactory _playerFactory;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private PlayerCollicionDetector _playerCollicionDetector;

    public Level(PlayerFactory playerFactory) => 
        _playerFactory = playerFactory;

    public void Start()
    {
        _playerModel = _playerFactory.CreatePlayerModel();
        
        _playerView = _playerFactory.CreatePlayerView();
        _playerView.Init(_playerModel);
        
        _playerCollicionDetector = _playerFactory.CreateCollisionDetector();
        _playerCollicionDetector.Init(_playerModel);
    }
}