mergeInto(LibraryManager.library, {
    AdsViewFullScreen: function () {
      console.log('Ads full screen.');
          ysdk.adv.showFullscreenAdv({
          callbacks: {
              onOpen: () => {
                SendMessage("Music", "StopMusic");
              },
              onClose: function(wasShown) {
                SendMessage("Music", "StartMusic");
                // some action after close
              },
              onError: function(error) {
                SendMessage("Music", "StartMusic");
              }
          }
      })
    },
      AdsViewRewards: function () {
        ysdk.adv.showRewardedVideo({
        callbacks: {
          onOpen: () => {
            console.log('Video ad open.');
            SendMessage("Music", "StopMusic");
          },
          onRewarded: () => {
            console.log('Rewarded!');
            SendMessage("Korol", "ShowResponse");
          },
          onClose: () => {
            console.log('Video ad closed.');
            SendMessage("Music", "StartMusic");
          }, 
          onError: (e) => {
            SendMessage("Music", "StartMusic");
            console.log('Error while open video ad:', e);
          }
        }
      })
    },
  });