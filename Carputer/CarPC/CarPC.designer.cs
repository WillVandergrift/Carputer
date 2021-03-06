﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarPC
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;

    using CarPC.Audio;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Carputer")]
	public partial class CarPCDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAlbum(Album instance);
    partial void UpdateAlbum(Album instance);
    partial void DeleteAlbum(Album instance);
    partial void InsertArtist(Artist instance);
    partial void UpdateArtist(Artist instance);
    partial void DeleteArtist(Artist instance);
    partial void InsertSong(Song instance);
    partial void UpdateSong(Song instance);
    partial void DeleteSong(Song instance);
    #endregion
		
		public CarPCDataContext() : 
				base(global::CarPC.Properties.Settings.Default.CarputerConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CarPCDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CarPCDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CarPCDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CarPCDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Album> Albums
		{
			get
			{
				return this.GetTable<Album>();
			}
		}
		
		public System.Data.Linq.Table<Artist> Artists
		{
			get
			{
				return this.GetTable<Artist>();
			}
		}
		
		public System.Data.Linq.Table<Song> Songs
		{
			get
			{
				return this.GetTable<Song>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Album")]
	public partial class Album : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _ArtistID;
		
		private string _Name;
		
		private System.Nullable<int> _Year;
		
		private string _ImagePath;
		
		private EntitySet<Song> _Songs;
		
		private EntityRef<Artist> _Artist;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnArtistIDChanging(int value);
    partial void OnArtistIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnYearChanging(System.Nullable<int> value);
    partial void OnYearChanged();
    partial void OnImagePathChanging(string value);
    partial void OnImagePathChanged();
    #endregion
		
		public Album()
		{
			this._Songs = new EntitySet<Song>(new Action<Song>(this.attach_Songs), new Action<Song>(this.detach_Songs));
			this._Artist = default(EntityRef<Artist>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArtistID", DbType="Int NOT NULL")]
		public int ArtistID
		{
			get
			{
				return this._ArtistID;
			}
			set
			{
				if ((this._ArtistID != value))
				{
					if (this._Artist.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnArtistIDChanging(value);
					this.SendPropertyChanging();
					this._ArtistID = value;
					this.SendPropertyChanged("ArtistID");
					this.OnArtistIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="Int")]
		public System.Nullable<int> Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this.OnYearChanging(value);
					this.SendPropertyChanging();
					this._Year = value;
					this.SendPropertyChanged("Year");
					this.OnYearChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImagePath", DbType="NVarChar(1024)")]
		public string ImagePath
		{
			get
			{
				return this._ImagePath;
			}
			set
			{
				if ((this._ImagePath != value))
				{
					this.OnImagePathChanging(value);
					this.SendPropertyChanging();
					this._ImagePath = value;
					this.SendPropertyChanged("ImagePath");
					this.OnImagePathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Album_Song", Storage="_Songs", ThisKey="ID", OtherKey="AlbumID")]
		public EntitySet<Song> Songs
		{
			get
			{
				return this._Songs;
			}
			set
			{
				this._Songs.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Artist_Album", Storage="_Artist", ThisKey="ArtistID", OtherKey="ID", IsForeignKey=true)]
		public Artist Artist
		{
			get
			{
				return this._Artist.Entity;
			}
			set
			{
				Artist previousValue = this._Artist.Entity;
				if (((previousValue != value) 
							|| (this._Artist.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Artist.Entity = null;
						previousValue.Albums.Remove(this);
					}
					this._Artist.Entity = value;
					if ((value != null))
					{
						value.Albums.Add(this);
						this._ArtistID = value.ID;
					}
					else
					{
						this._ArtistID = default(int);
					}
					this.SendPropertyChanged("Artist");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Songs(Song entity)
		{
			this.SendPropertyChanging();
			entity.Album = this;
		}
		
		private void detach_Songs(Song entity)
		{
			this.SendPropertyChanging();
			entity.Album = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Artist")]
	public partial class Artist : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Name;
		
		private string _ImagePath;
		
		private EntitySet<Album> _Albums;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnImagePathChanging(string value);
    partial void OnImagePathChanged();
    #endregion
		
		public Artist()
		{
			this._Albums = new EntitySet<Album>(new Action<Album>(this.attach_Albums), new Action<Album>(this.detach_Albums));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImagePath", DbType="NVarChar(1025)")]
		public string ImagePath
		{
			get
			{
				return this._ImagePath;
			}
			set
			{
				if ((this._ImagePath != value))
				{
					this.OnImagePathChanging(value);
					this.SendPropertyChanging();
					this._ImagePath = value;
					this.SendPropertyChanged("ImagePath");
					this.OnImagePathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Artist_Album", Storage="_Albums", ThisKey="ID", OtherKey="ArtistID")]
		public EntitySet<Album> Albums
		{
			get
			{
				return this._Albums;
			}
			set
			{
				this._Albums.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Albums(Album entity)
		{
			this.SendPropertyChanging();
			entity.Artist = this;
		}
		
		private void detach_Albums(Album entity)
		{
			this.SendPropertyChanging();
			entity.Artist = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Song")]
	public partial class Song : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _AlbumID;
		
		private string _Name;
		
		private string _Path;
		
		private string _Genre;
		
		private System.Nullable<int> _ListenCount;
		
		private System.Nullable<bool> _Favorite;
		
		private System.Nullable<bool> _Dislike;
		
		private EntityRef<Album> _Album;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnAlbumIDChanging(int value);
    partial void OnAlbumIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnPathChanging(string value);
    partial void OnPathChanged();
    partial void OnGenreChanging(string value);
    partial void OnGenreChanged();
    partial void OnListenCountChanging(System.Nullable<int> value);
    partial void OnListenCountChanged();
    partial void OnFavoriteChanging(System.Nullable<bool> value);
    partial void OnFavoriteChanged();
    partial void OnDislikeChanging(System.Nullable<bool> value);
    partial void OnDislikeChanged();
    #endregion
		
		public Song()
		{
			this._Album = default(EntityRef<Album>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AlbumID", DbType="Int NOT NULL")]
		public int AlbumID
		{
			get
			{
				return this._AlbumID;
			}
			set
			{
				if ((this._AlbumID != value))
				{
					if (this._Album.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAlbumIDChanging(value);
					this.SendPropertyChanging();
					this._AlbumID = value;
					this.SendPropertyChanged("AlbumID");
					this.OnAlbumIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Path", DbType="NVarChar(1024) NOT NULL", CanBeNull=false)]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				if ((this._Path != value))
				{
					this.OnPathChanging(value);
					this.SendPropertyChanging();
					this._Path = value;
					this.SendPropertyChanged("Path");
					this.OnPathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Genre", DbType="NVarChar(255)")]
		public string Genre
		{
			get
			{
				return this._Genre;
			}
			set
			{
				if ((this._Genre != value))
				{
					this.OnGenreChanging(value);
					this.SendPropertyChanging();
					this._Genre = value;
					this.SendPropertyChanged("Genre");
					this.OnGenreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ListenCount", DbType="Int")]
		public System.Nullable<int> ListenCount
		{
			get
			{
				return this._ListenCount;
			}
			set
			{
				if ((this._ListenCount != value))
				{
					this.OnListenCountChanging(value);
					this.SendPropertyChanging();
					this._ListenCount = value;
					this.SendPropertyChanged("ListenCount");
					this.OnListenCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Favorite", DbType="Bit")]
		public System.Nullable<bool> Favorite
		{
			get
			{
				return this._Favorite;
			}
			set
			{
				if ((this._Favorite != value))
				{
					this.OnFavoriteChanging(value);
					this.SendPropertyChanging();
					this._Favorite = value;
					this.SendPropertyChanged("Favorite");
					this.OnFavoriteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dislike", DbType="Bit")]
		public System.Nullable<bool> Dislike
		{
			get
			{
				return this._Dislike;
			}
			set
			{
				if ((this._Dislike != value))
				{
					this.OnDislikeChanging(value);
					this.SendPropertyChanging();
					this._Dislike = value;
					this.SendPropertyChanged("Dislike");
					this.OnDislikeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Album_Song", Storage="_Album", ThisKey="AlbumID", OtherKey="ID", IsForeignKey=true)]
		public Album Album
		{
			get
			{
				return this._Album.Entity;
			}
			set
			{
				Album previousValue = this._Album.Entity;
				if (((previousValue != value) 
							|| (this._Album.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Album.Entity = null;
						previousValue.Songs.Remove(this);
					}
					this._Album.Entity = value;
					if ((value != null))
					{
						value.Songs.Add(this);
						this._AlbumID = value.ID;
					}
					else
					{
						this._AlbumID = default(int);
					}
					this.SendPropertyChanged("Album");
				}
			}
		}

        public System.Windows.Media.ImageSource CoverArt
        {
            get
            {
                return CarPC.Audio.TagManager.GetCoverArt(_Path);
            }
        }
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
