mergeInto(LibraryManager.library, {

  Dispatch: function (cmd, payload) {
    if (window.Dispatch)
      window.Dispatch(Pointer_stringify(cmd), Pointer_stringify(payload));
    else {
      window.Dispatch = function (a, b) { console.log(Pointer_stringify(a), Pointer_stringify(b)) };
      unityInstance.SendMessage("DataManager", "GetPlayerData");
    }
  },
  Back : function (){
    window.history.back();
  },

});
