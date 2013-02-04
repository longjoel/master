/*

Copyright (c) 2012, Joel Longanecker (Joel.Longanecker@gmail.com)
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


*/




using System;
using System.IO;

using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;

namespace Sunfish.Messaging
{
	/// <summary>
	/// Message hub.
	/// </summary>
	public class MessageHub
	{

		public delegate void MessageRecievedDelegate (Message m);

		Dictionary<Type, MessageRecievedDelegate> _messageTypeRoutingTable;
		string _pipePath;
		long _seekDistance;


		/// <summary>
		/// Initializes a new instance of the <see cref="Sunfish.Messaging.MessageHub"/> class.
		/// </summary>
		/// <param name='pipePath'>
		/// Pipe path.
		/// </param>
		public MessageHub (string pipePath)
		{
			_pipePath = pipePath;
	
			ResetFile ();

			_messageTypeRoutingTable = new Dictionary<Type, MessageRecievedDelegate> ();
		}



		/// <summary>
		/// Resets the file.
		/// </summary>
		void ResetFile ()
		{
			if (File.Exists (_pipePath)) {
				File.Delete (_pipePath);

			}
			_seekDistance = 0;
		}

		/// Registers the route.
		/// </summary>
		/// <param name='messageType'>
		/// Message type.
		/// </param>
		/// <param name='route'>
		/// Route.
		/// </param>
		/// <param name='m'>
		/// M.
		/// </param>
		public void RegisterRoute (Type messageType, MessageRecievedDelegate m)
		{
			if (typeof(Message).IsAssignableFrom (messageType)) {
				_messageTypeRoutingTable [messageType] = m;
			} else {
				throw new ApplicationException ("Parameter 'message' is not type of Message");
			}
		}

		/// <summary>
		/// Unregisters the route.
		/// </summary>
		/// <param name='messageType'>
		/// Message type.
		/// </param>
		/// <param name='route'>
		/// Route.
		/// </param>
		/// <param name='m'>
		/// M.
		/// </param>
		public void UnregisterRoute (Type messageType, MessageRecievedDelegate m)
		{
			if (typeof(Message).IsAssignableFrom (messageType)) {
				if (_messageTypeRoutingTable.ContainsKey (messageType)) {

					_messageTypeRoutingTable .Remove (messageType);
				} else {
					throw new ApplicationException ("No call back assoiated with message type and route");
				}
			} else {
				throw new ApplicationException ("Parameter 'message' is not type of Message");
			}
		}

		/// <summary>
		/// Waits the message.
		/// </summary>
		/// <returns>
		/// The message.
		/// </returns>
		public Message WaitMessage ()
		{
			var bf = new BinaryFormatter ();

			double fLength = 0;
			if (File.Exists (_pipePath)) {
				fLength = new FileInfo (_pipePath).Length;
			}
			Message m = null;
			if (_seekDistance < fLength) {
				using (var fptr = new FileStream(_pipePath, FileMode.OpenOrCreate, FileAccess.Read)) {
					fptr.Seek (_seekDistance, SeekOrigin.Begin);
				
					m = (Message)bf.Deserialize (fptr);
					_seekDistance = fptr.Position;

					fptr.Flush ();
				}
				
			} else {
				ResetFile ();
			}



			return m;
		}

		/// <summary>
		/// Polls the message.
		/// </summary>
		/// <returns>
		/// The message.
		/// </returns>
		public Message PollMessage ()
		{
			Message m = null;
		

			var bf = new BinaryFormatter ();
			double fLength = 0;
			if (File.Exists (_pipePath)) {
				fLength = new FileInfo (_pipePath).Length;
			}
			if (fLength > 0) {
				if (_seekDistance < fLength) {
					using (var fptr = new FileStream(_pipePath, FileMode.OpenOrCreate, FileAccess.Read)) {
						fptr.Seek (_seekDistance, SeekOrigin.Begin);
				
						m = (Message)bf.Deserialize (fptr);
						_seekDistance = fptr.Position;

						fptr.Flush ();
					}
				
				} else {
					ResetFile ();

				}

			}
			return m;

		}

		/// <summary>
		/// Dispatchs the message.
		/// </summary>
		/// <param name='m'>
		/// M.
		/// </param>
		public void DispatchMessage (Message m)
		{
			_messageTypeRoutingTable [m.GetType ()] (m);
		}


		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name='route'>
		/// Route.
		/// </param>
		/// <param name='m'>
		/// M.
		/// </param>
		public static void SendMessage (string route, Message m)
		{
			if (! File.Exists (route)) {
				File.Create (route);
			}

			var bf = new BinaryFormatter ();

			while (true) {

				try {
					using (var streamWriter = new FileStream(route, FileMode.Append, FileAccess.Write)) {

						bf.Serialize (streamWriter, m);
						streamWriter.Flush ();
					}
					break;
				} catch {
				}
			}

		}
	}


}


