class CreateUsers < ActiveRecord::Migration
  def self.up
    create_table "users", :force => true do |t|
      t.column :login,                     :string
      t.column :email,                     :string
      t.column :crypted_password,          :string, :limit => 40
      t.column :salt,                      :string, :limit => 40
      t.column :created_at,                :datetime
      t.column :updated_at,                :datetime
      t.column :remember_token,            :string
      t.column :remember_token_expires_at, :datetime
    end
    if RAILS_ENV == "development"
       u=User.create :login => "usuario1", :password => "password4user1", 
                     :password_confirmation => "password4user1", :email => "usuario1@at.com"
       u.save!
    end
  end

  def self.down
    drop_table "users"
  end
end
