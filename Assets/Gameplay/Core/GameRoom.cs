using Gameplay.Behaviours.UI;
using Gameplay.Core.Actions;
using Gameplay.Core.Cards;
using UnityEngine;

namespace Gameplay.Core
{
    public class GameRoom : MonoBehaviour
    {
        [SerializeField] Arena arena;
        [SerializeField] MatchReferee matchReferee;
        [SerializeField] CardPrefabMap cardPrefabMap;
        [SerializeField] GameplayHUD gameplayHUD;
        [SerializeField] GameObject playerPrefab;
        [SerializeField] GestureRecognizer gestureRecognizer;

        IPlayer HomePlayer { get; set; }

        IPlayer VisitorPlayer { get; set; }

        Dealer Dealer { get; } = new Dealer();

        public float maxTime = 20;
        public float minTime = 2;
        
        private float time;

     //The time to spawn the object
        public float spawnTime;
        void Start()
        {
            var gameObjectFactory = new GameObjectFactory(cardPrefabMap, playerPrefab);
            var deployer = new Deployer(gameObjectFactory, arena, gameplayHUD);
            var gameActionFactory = new GameActionFactory(deployer);

            gameplayHUD.Setup(gestureRecognizer, arena);
            gameplayHUD.CreateCardSlots(PlayerHand.HandSize);

            HomePlayer = gameObjectFactory.CreatePlayer(Team.Home);
            VisitorPlayer = gameObjectFactory.CreatePlayer(Team.Visitor);

            HomePlayer.Setup(PlayerDeck.GetDummyDeck());
            VisitorPlayer.Setup(PlayerDeck.GetDummyDeck());

            Dealer.DealInitialCards(HomePlayer);
            Dealer.DealInitialCards(VisitorPlayer);

            matchReferee.Setup(gameActionFactory, players: new []{HomePlayer, VisitorPlayer});
            
            SetRandomTime();
            time = minTime;
        }

         void SetRandomTime(){
         spawnTime = Random.Range(minTime, maxTime);
         Debug.Log ("Next object spawn in " + spawnTime + " seconds.");
     }

        void Awake() => AddObservers();

        
        void OnDestroy() => RemoveObservers();

        void AddObservers()
        {
            Dealer.OnDealtCard += gameplayHUD.OnCardDealt;
            gameplayHUD.OnUseCard += OnHomePlayerUsedCard;
        }

        void Update(){
            time += Time.deltaTime;

            if(time >= spawnTime){
                Dealer.DealCard(VisitorPlayer);
                OnVisitorUsedCard();
                time = 0;
            }
        }

        void RemoveObservers()
        {
            Dealer.OnDealtCard -= gameplayHUD.OnCardDealt;
            gameplayHUD.OnUseCard -= OnHomePlayerUsedCard;
        }

        void OnHomePlayerUsedCard(CardType card, int laneIdx)
        {

            matchReferee.OnPlayerUsedCard(card, HomePlayer.Team, laneIdx);
            Dealer.DealCard(HomePlayer);
            
        }

        void OnVisitorUsedCard(){
            CardType Card = VisitorPlayer.Hand.Cards[0];
            matchReferee.OnPlayerUsedCard(Card, VisitorPlayer.Team, 0);
            
        }
    }
}
